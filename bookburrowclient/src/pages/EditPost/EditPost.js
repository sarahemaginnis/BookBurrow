import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router";
import { Button, Container, Row, Col, Form, InputGroup, Modal } from "react-bootstrap";
import UploadWidget from "../../components/UploadWidget";
import { signOutOfFirebase } from "../../utils/auth";
import './EditPost.css';
import { Avatar } from "@mui/material";

export default function EditPostPage ({user, currentUser}) {
    const {postId} = useParams(); //variable storing the route parameter
    const [userPostObject, setUserPostObject] = useState({});
    const [show, setShow] = useState(false);
    const [books, syncBooks] = useState([]);
    const [postTypes, syncPostTypes] = useState([]);
    const [postTypeName, setPostTypeName] = useState("");
    const [postTypeValue, setPostTypeValue] = useState("");

    const handleClose = () => setShow(false);

    const navigate = useNavigate();

    const navigateHome = () => {
        navigate(`/`)
    }

    const cancelEdits = () => {
        navigate(`/post/${postId}`)
    }

    const submitEdits = () => {
        navigate(`/post/${postId}`)
    }

    //Fetch all post Types

    //Fetch all books
    useEffect(() => {
        fetch(`https://localhost:7210/api/Book`, {
            method: "GET",
            headers: {
              "Access-Control-Allow-Origin": "https://localhost:7210",
              "Content-Type": "application/json",
            },
        })
        .then((res) => res.json())
        .then((data) => {
            syncBooks(data);
        })
    }, []);

    //Get userPost information from API
    useEffect(() => {
        fetch(`https://localhost:7210/api/UserPost/${postId}`, {
            method: "GET",
            headers: {
              "Access-Control-Allow-Origin": "https://localhost:7210",
              "Content-Type": "application/json",
            },
        })
        .then((res) => res.json())
        .then((data) => {
            setUserPostObject(data);
        });
    }, []);

    //Update userPost
    const updateUserPost = (evt) => {
        evt.preventDefault();
        //Construct a new object to replace the existing one in the API
        const updatedUserPostObject = {
            id: userPostObject.id,
            userId: userPostObject.userId,
            userProfile: {
                id: userPostObject.userProfile.id,
                userId: userPostObject.userProfile.userId,
                profileImageUrl:userPostObject.userProfile.profileImageUrl,
                firstName:userPostObject.userProfile.firstName,
                lastName:userPostObject.userProfile.lastName,
                handle: userPostObject.userProfile.handle,
                pronounId:userPostObject.userProfile.pronounId,
                userPronoun: {
                    id: userPostObject.userProfile.userPronoun.id,
                    pronouns: userPostObject.userProfile.userPronoun.pronouns,
                },
                biography: userPostObject.userProfile.biography,
                biographyUrl: userPostObject.userProfile.biographyUrl,
                birthday: userPostObject.userProfile.birthday,
                createdAt: userPostObject.userProfile.createdAt,
                updatedAt: userPostObject.userProfile.updatedAt,
            },
            postType: {
                name: userPostObject.postType.name,
                value: userPostObject.postType.value,
            },
            bookId: userPostObject.bookId,
            book: {
                id: userPostObject.book.id,
                title: userPostObject.book.title,
                isbn: userPostObject.book.isbn,
                description: userPostObject.book.description,
                coverImageUrl: userPostObject.book.coverImageUrl,
                datePublished: userPostObject.book.datePublished,
                createdAt: userPostObject.book.createdAt,
                updatedAt: userPostObject.book.updatedAt,
            },
            title: userPostObject.title,
            cloudinaryUrl: userPostObject.cloudinaryUrl,
            caption: userPostObject.caption,
            source: userPostObject.source,
            songUrl: userPostObject.songUrl,
            songUrlSummary: userPostObject.songUrlSummary,
            createdAt: userPostObject.createdAt,
            updatedAt: Date.now,
        };
        //Perform the PUT request to replace the object
        fetch(`https://localhost:7210/api/UserPost/${postId}`, {
            method: "PUT",
            headers: {
                "Access-Control-Allow-Origin": "https://localhost:7210",
                "Content-Type": "application/json",
            },
            body: JSON.stringify(updatedUserPostObject), 
        }).then(() => {
            submitEdits()
        });
    }
    return (
        currentUser.id === userPostObject.userId ? <> 
        <Container>
              <Row>
                <Col sm={2}>
                    <Avatar 
                        src={userPostObject.userProfile.profileImageUrl}
                        alt={userPostObject.userProfile.handle}
                        sx={{width: 64, height: 64}}
                        variant="rounded"
                    />
                    {/* <img src={userPostObject.userProfile.profileImageUrl} alt={userPostObject.userProfile.handle} /> */}
                </Col>
                <Col>
                    <p><b>{userPostObject.userProfile.handle}</b></p>
                </Col>
              </Row>
            <Form>
              <Row>
                <Col>
                    <Form.Group className="mb-3" controlId="formBasicTitle">
                        <Form.Label>Title</Form.Label>
                        <Form.Control type="text" placeholder="Title" defaultValue={userPostObject.title} onChange={
                        (event) => {
                            const copy = {...userPostObject} 
                            copy.title = event.target.value 
                            setUserPostObject(copy)}}  />
                    </Form.Group>
                </Col>
              </Row>
              <Row className="edit_post_image" >
                <Col>
                <img src={userPostObject.cloudinaryUrl} />
                </Col>
              </Row>
              <Row>
                <Col>
                    <Form.Group className="mb-3" controlId="formBasicCaption">
                        <Form.Label>Caption</Form.Label>
                        <Form.Control type="text" placeholder="Your text here" defaultValue={userPostObject.caption} onChange={
                        (event) => {
                            const copy = {...userPostObject} 
                            copy.caption = event.target.value 
                            setUserPostObject(copy)}}  />
                    </Form.Group>
                </Col>
              </Row>
              <Row>
                <Col>
                <Form.Group className="mb-3" controlId="formBasicBook">
                    <Form.Label>Tag a Book?</Form.Label>
                    <Form.Control as="select" onChange={
                        (event) => {
                            const copy = {...userPostObject} 
                            copy.bookId = parseInt(event.target.value) 
                            copy.book.id = parseInt(event.target.value) 
                            copy.book.title = event.target.options[event.target.selectedIndex].innerHTML
                            const selectedUserBookObject = books.find(b => b.title === copy.book.title)
                            copy.book.isbn = selectedUserBookObject.isbn
                            copy.book.description = selectedUserBookObject.description,
                            copy.book.coverImageUrl = selectedUserBookObject.coverImageUrl,
                            copy.book.datePublished = selectedUserBookObject.datePublished,
                            copy.book.createdAt = selectedUserBookObject.createdAt,
                            copy.book.updatedAt = selectedUserBookObject.updatedAt,
                            setUserPostObject(copy)
                            }
                        } 
                        value={userPostObject.bookId}>
                        {books.map((b) => {return(
                            <option key={`book--${b.id}`} value={b.id}>
                            {b.title}
                            </option>
                        )})}
                    </Form.Control>
                    </Form.Group>
                </Col>
              </Row>
            </Form>
            <Row>
            <Col sm={1}>
                <Button className="btn__btn-secondary" type="button" onClick={cancelEdits}>Cancel</Button>
            </Col>
            <Col>
                <Button type="submit" onClick={updateUserPost}>Save</Button>
            </Col>
        </Row>
          </Container>
        </> : null
    )
}