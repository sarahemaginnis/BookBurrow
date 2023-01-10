import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router";
import { Button, Container, Row, Col, Form, InputGroup, Modal } from "react-bootstrap";
import UploadWidget from "../../components/UploadWidget";
import { signOutOfFirebase } from "../../utils/auth";
import './EditUserProfile.css';

export default function EditUserProfile ({user, currentUser}) {
    const {userId} = useParams(); //variable storing the route parameter
    const [userProfileObject, setUserProfileObject] = useState({});
    const [userProfileImage, setUserProfileImage] = useState(""); //state variable for profileImage
    const [pronouns, syncPronouns] = useState([]); //state variable for array of pronouns
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    
    console.log(userProfileObject);
    console.log(userProfileObject.pronounId);
    console.log(userProfileImage);

    //Function that gets passed down to UploadWidget
    const pullData = (data) => {
        console.log(data);
        setUserProfileImage(data);
        console.log(userProfileImage)
    }

    //Fetch all pronouns
    useEffect(() => {
        fetch(`https://localhost:7210/api/UserPronoun`, {
            method: "GET",
            headers: {
              "Access-Control-Allow-Origin": "https://localhost:7210",
              "Content-Type": "application/json",
            },
        })
        .then((res) => res.json())
        .then((data) => {
            syncPronouns(data);
        })
    }, []);

    //Get userProfile information from API
    useEffect(() => {
        fetch(`https://localhost:7210/api/UserProfile/Get/${userId}`, {
            method: "GET",
            headers: {
              "Access-Control-Allow-Origin": "https://localhost:7210",
              "Content-Type": "application/json",
            },
        })
        .then((res) => res.json())
        .then((profile) => {
            setUserProfileObject(profile)
            setUserProfileImage(profile.profileImageUrl)
            console.log(userProfileObject)
        })
    }, []);

    //Delete userprofile first, then delete user
    const DeleteProfileAndUser = (id) => {
        fetch(`https://localhost:7210/api/UserProfile/Delete/${id}`, {
            method: "DELETE"
        })
        .then(() => {
            return fetch(`https://localhost:7210/api/User/${userProfileObject.userId}`, {
                method: "DELETE"
            })
        })
        .then(() => {
            handleClose()
            signOutOfFirebase()
            loginPage()
            // window.location.reload(true)
        })
    }

    const navigate = useNavigate();
    
    const cancelEdits = () => {
    navigate(`/user/${userProfileObject.userId}`)
    }

    const submitEdits = () => {
        navigate(`/user/${userProfileObject.userId}`)
    }

    const loginPage = () => {
        navigate(`/`)
    }

    const UpdateUserProfile = (evt) => {
        evt.preventDefault();
        //Construct a new object to replace the existing one in the API
        const updatedUserProfile = {
            id: userProfileObject.id,
            userId: userProfileObject.userId,
            profileImageUrl: userProfileImage,
            firstName: userProfileObject.firstName,
            lastName: userProfileObject.lastName,
            handle: userProfileObject.handle,
            pronounId: userProfileObject.pronounId,
            userPronoun: {
                id: userProfileObject.pronounId,
                pronouns: userProfileObject.userPronoun.pronouns,
            },
            biography: userProfileObject.biography,
            biographyUrl: userProfileObject.biographyUrl,
            birthday: userProfileObject.birthday,
            createdAt: userProfileObject.createdAt,
            updatedAt: Date.now,
        };
        //Perform the PUT request to replace the object
        fetch(`https://localhost:7210/api/UserProfile/Put/${userId}`, {
            method: "PUT",
            headers: {
                "Access-Control-Allow-Origin": "https://localhost:7210",
                "Content-Type": "application/json",
            },
            body: JSON.stringify(updatedUserProfile),
        }).then(() => {
            submitEdits()
        });
    }

    return (   
    currentUser.id === userProfileObject.userId ?
    <>
    <Container>
        <Row>
            <Col sm={1}>
                <Button className="btn__btn-secondary" type="button" onClick={cancelEdits}>Cancel</Button>
            </Col>
            <Col>
                <Button type="submit" onClick={UpdateUserProfile}>Save</Button>
            </Col>
        </Row>
        <Row>
            <Col>
                <h2>{userProfileObject.firstName} {userProfileObject.lastName}</h2>
            </Col>
        </Row>
        <Row>
            <Col>
                <img src={`${userProfileObject.id ? userProfileImage : null}`} className="profile_image"/>
            </Col>
        </Row>
        <Row>
            <Col>
                <UploadWidget func={pullData} />        
            </Col>
        </Row>
        <Row>
            <Col>
                <Form>
                <Form.Group className="mb-3" controlId="formBasicFirstName">
                    <Form.Label>First Name</Form.Label>
                    <Form.Control type="text" placeholder="First name" defaultValue={userProfileObject.firstName} onChange={
                        (event) => {
                            const copy = {...userProfileObject} 
                            copy.firstName = event.target.value 
                            setUserProfileObject(copy)}} 
                        />
                </Form.Group>
                <Form.Group className="mb-3" controlId="formBasicLastName">
                    <Form.Label>Last Name</Form.Label>
                    <Form.Control type="text" placeholder="Last name" defaultValue={userProfileObject.lastName} onChange={
                        (event) => {
                            const copy = {...userProfileObject} 
                            copy.lastName = event.target.value 
                            setUserProfileObject(copy)}}
                        />
                </Form.Group>
                <Form.Group className="mb-3" controlId="formBasicHandle">
                    <Form.Label>Handle</Form.Label>
                    <InputGroup>
                    <InputGroup.Text id="inputGroupPrepend">@</InputGroup.Text>
                    </InputGroup>
                    <Form.Control type="text" placeholder={userProfileObject.handle} defaultValue={userProfileObject.handle} onChange={
                        (event) => {
                            const copy = {...userProfileObject} 
                            copy.handle = event.target.value 
                            setUserProfileObject(copy)}}
                        />
                </Form.Group>
                <Form.Group>
                    <Form.Control as="select" onChange={
                        (event) => {
                            const copy = {...userProfileObject} 
                            copy.pronounId = parseInt(event.target.value) 
                            copy.userPronoun.id = parseInt(event.target.value) 
                            copy.userPronoun.pronouns = event.target.options[event.target.selectedIndex].innerHTML
                            setUserProfileObject(copy)
                            }
                        } 
                        value={userProfileObject.pronounId}>
                        {pronouns.map((p) => {return(
                            <option key={`pronoun--${p.id}`} value={p.id}>
                            {p.pronouns}
                            </option>
                        )})}
                        </Form.Control>
                </Form.Group>
                <Form.Group className="mb-3" controlId="formBasicBiography">
                    <Form.Label>Biography</Form.Label>
                    <Form.Control as="textarea" rows={3} defaultValue={userProfileObject.biography} onChange={
                        (event) => {
                            const copy = {...userProfileObject} 
                            copy.biography = event.target.value 
                            setUserProfileObject(copy)}
                        } />
                </Form.Group>
                <Form.Group className="mb-3" controlId="formBasicBiographyUrl">
                    <Form.Label>Featured Link (url)</Form.Label>
                    <Form.Control type="text" placeholder={userProfileObject.biographyUrl} defaultValue={userProfileObject.biographyUrl} onChange={
                        (event) => {
                            const copy = {...userProfileObject} 
                            copy.biographyUrl = event.target.value 
                            setUserProfileObject(copy)}
                        } />
                </Form.Group>
                </Form>
            </Col>
        </Row>
        <Row>
            <Col>
                <h3>Delete Account</h3>
            </Col>
        </Row>
        <Row>
            <Col>
                <p>Deleting your account is irreversible.</p>
            </Col>
        </Row>
        <Row>
            <Col>
                <Button onClick={(e) => {
                    e.stopPropagation();
                    setShow(true)
                    }}
                    className="btn-btn__primary">Delete Account</Button>
            </Col>
        </Row>
    </Container>
    <Modal show={show} onHide={handleClose} size="lg" centered className="modal__delete">
        <Modal.Header className="modal__header" closeButton></Modal.Header>
        <Modal.Body className="modal__body">
            <h4>Are you sure you want to delete your account?</h4>
            <p>This action cannot be undone.</p>
        </Modal.Body>
        <Modal.Footer className="modal__footer">
            <Button className="btn__btn-secondary" onClick={(e) => {
                e.stopPropagation();
                setShow(false)
            }}>Cancel</Button>
            <Button className="btn__btn-primary" onClick={() => DeleteProfileAndUser(userId)}>Delete</Button>
        </Modal.Footer>
    </Modal>
    </> : null
    )
}