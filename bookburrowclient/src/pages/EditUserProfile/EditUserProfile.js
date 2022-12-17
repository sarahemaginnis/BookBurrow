import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router";
import { Button, Container, Row, Col, Form, InputGroup } from "react-bootstrap";
import UploadWidget from "../../components/UploadWidget";
import { signOutOfFirebase } from "../../utils/auth";
import './EditUserProfile.css';

export default function EditUserProfile ({user, currentUser}) {
    const {userId} = useParams(); //variable storing the route parameter
    const [userProfileObject, setUserProfileObject] = useState({});
    const [pronouns, syncPronouns] = useState([]); //state variable for array of pronouns

    const [date, setDate] = useState(new Date()); //updated date
    
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
        fetch(`https://localhost:7210/api/UserProfile/${userId}`, {
            method: "GET",
            headers: {
              "Access-Control-Allow-Origin": "https://localhost:7210",
              "Content-Type": "application/json",
            },
        })
        .then((res) => res.json())
        .then((profile) => {
            setUserProfileObject(profile)
            console.log(userProfileObject)
        })
    }, []);

    const Delete = () => {
        fetch(`https://localhost:7210/api/UserProfile/${userId}`, {
            method: "DELETE"
        })
        .then({signOutOfFirebase})
    }

    const navigate = useNavigate();
    
    const cancelEdits = () => {
    navigate(`/user/${userProfileObject.userId}`)
    }

    const submitEdits = () => {
        navigate(`/user/${userProfileObject.userId}`)
    }

    const UpdateUserProfile = (evt) => {
        evt.preventDefault();
        //Construct a new object to replace the existing one in the API
        const updatedUserProfile = {
            userId: userProfileObject.userId,
            profileImageUrl: userProfileObject.profileImageUrl,
            firstName: userProfileObject.firstName,
            lastName: userProfileObject.lastName,
            handle: userProfileObject.handle,
            pronounId: userProfileObject.pronounId,
            biography: userProfileObject.biography,
            biographyUrl: userProfileObject.biographyUrl,
            birthday: userProfileObject.birthday,
            createdAt: userProfileObject.createdAt,
            updatedAt: date,
        };
        //Perform the PUT request to replace the object
        fetch(`https://localhost:7210/api/UserProfile/${userId}`, {
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
    currentUser ?
    <>
    <Container>
        <Row>
            <Col>
                <Button type="button" onClick={cancelEdits}>Cancel</Button>
            </Col>
            <Col>
                <Button type="submit" onClick={UpdateUserProfile}>Save</Button>
            </Col>
        </Row>
        <Row>
            <Col>
                <h2>User First Name & Last Name</h2>
            </Col>
        </Row>
        <Row>
            <Col>
                <h3>Profile Image</h3>
            </Col>
        </Row>
        <Row>
            <Col>
                <UploadWidget />        
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
                    <Form.Select aria-label="Preferred Pronouns" value={userProfileObject.pronounId} defaultValue={userProfileObject.pronounId} onChange={
                        (event) => {
                            const copy = {...userProfileObject} 
                            copy.pronounId = event.target.value 
                            setUserProfileObject(copy)}
                    }>
                        {pronouns.map((e) => {return(
                            <option key={`pronoun--${e.id}`} value={e.id}>
                            {e.pronouns}
                            </option>
                        )})}
                    </Form.Select>
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
                <Form.Group className="mb-3" controlId="formUpdatedDate">
                    <Form.Label>Today's Date</Form.Label>
                    <Form.Control type="date" placeholder="Today's Date" value={date} onChange={(e) => setDate(e.target.value)} />
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
                <Button onClick={Delete}>Delete Account</Button>
            </Col>
        </Row>
    </Container>
    </> : null
    )
}