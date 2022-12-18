import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Form, Button, InputGroup } from "react-bootstrap";

export default function CreatePost({user, currentUser, userProfile, show }) {
    const [postTypes, syncPostTypes] = useState([]);
    const [books, syncBooks] = useState([]);
    const [postTypeName, setPostTypeName] = useState("");
    const [postTypeValue, setPostTypeValue] = useState("");
    const [postBookId, setPostBookId] = useState("")
    const [postBook, setPostBook] = useState({})
    const [postTitle, setPostTitle] = useState("");
    const [postCloudinaryUrl, setPostCloudinaryUrl] = useState("");
    const [postCaption, setPostCaption] = useState("");
    const [postSource, setPostSource] = useState("");
    const [postSongUrl, setPostSongUrl] = useState("");
    const [postSongUrlSummary, setPostSongUrlSummary] = useState("");

    const navigation = useNavigate();
  
    const navigateToDashboard = () => {
      navigation('/')
    }

    //Fetch all post types

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

    const CreateNewPost = () => {
        const newPostObject = {
            userId: currentUser.id,
            userProfile: {
                id: userProfile.id,
                userId: userProfile.userId,
                profileImageUrl:userProfile.profileImageUrl,
                firstName:userProfile.firstName,
                lastName:userProfile.lastName,
                handle: userProfile.handle,
                pronounId:userProfile.pronounId,
                userPronoun: {
                    id: userProfile.userPronoun.id,
                    pronouns: userProfile.userPronoun.pronouns,
                },
                biography: userProfile.biography,
                biographyUrl: userProfile.biographyUrl,
                birthday: userProfile.birthday,
                createdAt: userProfile.createdAt,
                updatedAt: userProfile.updatedAt,
            },
            postType: {
                name: postTypeName,
                value: postTypeValue,
            },
            bookId: postBookId,
            book: {
                id: postBook.id,
                title: postBook.title,
                isbn: postBook.isbn,
                description: postBook.description,
                coverImageUrl: postBook.coverImageUrl,
                datePublished: postBook.datePublished,
                createdAt: postBook.createdAt,
                updatedAt: postBook.updatedAt,
            },
            title: postTitle,
            cloudinaryUrl: postCloudinaryUrl,
            caption: postCaption,
            source: postSource,
            songUrl: postSongUrl,
            songUrlSummary: postSongUrlSummary,
        }
    const fetchOptions = {
        method: 'POST',
        headers: {
            "Access-Control-Allow-Origin": "https://localhost:7210",
            "Content-Type": "application/json",
        },
        body: JSON.stringify(newPostObject)
    }
    return fetch('https://localhost:7210/api/UserPost', fetchOptions)
    .then(res => res.json())
    .then(() => {navigateToDashboard()})
    }

    return (
        user ? 
        <>
        <Modal show={show} onHide={!show} size="lg" centered className="modal__new">
        <Modal.Header className="modal__header" closeButton></Modal.Header>
        <Modal.Body className="modal__body">
          <Container>
              <Row>
                <Col>
                    <img src={userProfile.profileImageUrl} alt={userProfile.handle} />
                </Col>
                <Col>
                    <p>{userProfile.handle}</p>
                </Col>
              </Row>
            <Form>
              <Row>
                <Col>
                    <Form.Group className="mb-3" controlId="formBasicTitle">
                        <Form.Label>Title</Form.Label>
                        <Form.Control type="text" placeholder="Title" defaultValue="Title" onChange={(event) => setPostTitle(event.target.value)} />
                    </Form.Group>
                </Col>
              </Row>
              <Row>
                <Col>
                    <Form.Group className="mb-3" controlId="formBasicCaption">
                        <Form.Label>Caption</Form.Label>
                        <Form.Control type="text" placeholder="Your text here" defaultValue="Your text here" onChange={(event) => setPostCaption(event.target.value)} />
                    </Form.Group>
                </Col>
              </Row>
              <Row>
                <Col>
                <Form.Group className="mb-3" controlId="formBasicBook">
                    <Form.Label>Tag a Book?</Form.Label>
                    <Form.Control as="select" onChange={
                        (event) => {
                        setPostBookId(parseInt(event.target.value))
                        setPostBook(event.target.options[event.target.selectedIndex].innerHTML)
                        }
                    }
                        value={postBookId}>
                        {books.map((e) => (
                            <option key={`book--${e.id}`}
                            value={e.id}
                            >
                                {e.title}
                            </option>
                        ))}
                    </Form.Control>
                    </Form.Group>
                </Col>
              </Row>
            </Form>
          </Container>
        </Modal.Body>
        <Modal.Footer className="modal__footer">
            <Button className="btn__btn-secondary" onClick={!show}>Close</Button>
            <Button className="btn__btn-primary" onClick={() => CreateNewPost()}>Post</Button>
        </Modal.Footer>
    </Modal>
        </> : null
    );
}