import React, {useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { signIntoFirebase, signOutOfFirebase } from '../utils/auth';
import {Container, Row, Col, Modal, Form, Button} from "react-bootstrap";
import {IoHeartSharp, IoPricetagsOutline} from "react-icons/io5";
import {GiMagicLamp} from "react-icons/gi";
import {GoTextSize} from "react-icons/go";
import {AiOutlineCamera} from "react-icons/ai";
import {HiChatBubbleLeftRight} from "react-icons/hi2";
import {FaHeadphonesAlt} from "react-icons/fa";
import {BsCameraVideoFill } from "react-icons/bs";
import {ImQuotesLeft, ImLink} from "react-icons/im";

export default function Authenticated({ user, currentUser }) {
  const [postTypes, syncPostTypes] = useState([]);
  const [books, syncBooks] = useState([]);
  const [userProfileObject, setUserProfileObject] = useState({});
  const [userBookObject, setUserBookObject] = useState({});
  const [postTypeName, setPostTypeName] = useState("Text");
  const [postTypeValue, setPostTypeValue] = useState(0);
  const [postBookId, setPostBookId] = useState("")
  const [postBookTitle, setPostBookTitle] = useState("")
  const [postTitle, setPostTitle] = useState("");
  const [postCloudinaryUrl, setPostCloudinaryUrl] = useState("");
  const [postCaption, setPostCaption] = useState("");
  const [postSource, setPostSource] = useState("");
  const [postSongUrl, setPostSongUrl] = useState("");
  const [postSongUrlSummary, setPostSongUrlSummary] = useState("");
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  console.log(currentUser);
  const buttonClick = () => {
    if (user){
      signOutOfFirebase()
    } else {
      signIntoFirebase()
    }
  }

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

    //Get userProfile information from API
    const GetUserProfile = () => {
      fetch(`https://localhost:7210/api/UserProfile/UserProfileByUserId/${currentUser.id}`, {
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
    }

    useEffect(() => {
        if(currentUser.hasOwnProperty("id")){GetUserProfile() ; console.log("getting User Profile")}
    }, [currentUser]);

    const CreateNewPost = () => {
        const newPostObject = {
            userId: currentUser.id,
            userProfile: {
                id: userProfileObject.id,
                userId: userProfileObject.userId,
                profileImageUrl:userProfileObject.profileImageUrl,
                firstName:userProfileObject.firstName,
                lastName:userProfileObject.lastName,
                handle: userProfileObject.handle,
                pronounId:userProfileObject.pronounId,
                userPronoun: {
                    id: userProfileObject.userPronoun.id,
                    pronouns: userProfileObject.userPronoun.pronouns,
                },
                biography: userProfileObject.biography,
                biographyUrl: userProfileObject.biographyUrl,
                birthday: userProfileObject.birthday,
                createdAt: userProfileObject.createdAt,
                updatedAt: userProfileObject.updatedAt,
            },
            postType: {
                name: postTypeName,
                value: postTypeValue,
            },
            bookId: userBookObject.id,
            book: {
                id: userBookObject.id,
                title: userBookObject.title,
                isbn: userBookObject.isbn,
                description: userBookObject.description,
                coverImageUrl: userBookObject.coverImageUrl,
                datePublished: userBookObject.datePublished,
                createdAt: userBookObject.createdAt,
                updatedAt: userBookObject.updatedAt,
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
    .then(handleClose)
    .then(() => {navigateToDashboard()})
    }

  return ( currentUser.hasOwnProperty("id") && userProfileObject.hasOwnProperty("userId") ?
  <>
    <Container>
      <Row>
        <Col sm={9}>
          <Row>
            <Col sm={3}>
              <img src={user.photoURL} alt={user.displayName} />
            </Col>
            <Col sm={6}>
              <Container>
            <Row>
                <Col><GoTextSize onClick={(e) => {
                    e.stopPropagation();
                    setShow(true)
                    }}  /></Col>
                <Col><AiOutlineCamera /></Col>
                <Col><ImQuotesLeft /></Col>
                <Col><ImLink /></Col>
                <Col><HiChatBubbleLeftRight /></Col>
                <Col><FaHeadphonesAlt /></Col>
                <Col><BsCameraVideoFill /></Col>
            </Row>
        </Container>
            </Col>
          </Row>
        <Row>
          <Col><h4>Following <IoHeartSharp /></h4></Col>
          <Col><h4>For you <GiMagicLamp /></h4></Col>
          <Col><h4>Your tags <IoPricetagsOutline /></h4></Col>
        </Row>
        <Row>
          <h2>Post Cards Go Here!</h2>
        </Row>
        </Col>
        <Col sm={3}>
          <h3>Currently Reading Book Cards go below!</h3>
        </Col>
      </Row>
    <div className="text-center mt-5">
      <h1>Welcome, {userProfileObject.handle}!</h1>
      <img
        referrerPolicy="no-referrer"
        src={userProfileObject.profileImageUrl}
        alt={userProfileObject.handle}
        />
      <h1>{userProfileObject.firstName} {userProfileObject.lastName}</h1>
      <h5>{userProfileObject.handle}</h5>
      <div className="mt-2">
        <button type="button" className="btn btn-danger" onClick={buttonClick}>
          {user ? "Sign Out" : "Sign In"}
        </button>
      </div>
    </div>
    <Modal show={show} onHide={handleClose} size="lg" centered className="modal__new">
        <Modal.Header className="modal__header" closeButton></Modal.Header>
        <Modal.Body className="modal__body">
          <Container>
              <Row>
                <Col>
                    <img src={userProfileObject.profileImageUrl} alt={userProfileObject.handle} />
                </Col>
                <Col>
                    <p>{userProfileObject.handle}</p>
                </Col>
              </Row>
            <Form>
              <Row>
                <Col>
                    <Form.Group className="mb-3" controlId="formBasicTitle">
                        <Form.Label>Title</Form.Label>
                        <Form.Control type="text" placeholder="Title" defaultValue="" onChange={(event) => setPostTitle(event.target.value)} />
                    </Form.Group>
                </Col>
              </Row>
              <Row>
                <Col>
                    <Form.Group className="mb-3" controlId="formBasicCaption">
                        <Form.Label>Caption</Form.Label>
                        <Form.Control as="textarea" placeholder="Your text here" defaultValue="" onChange={(event) => setPostCaption(event.target.value)} />
                    </Form.Group>
                </Col>
              </Row>
              <Row>
                <Col>
                <Form.Group className="mb-3" controlId="formBasicBook">
                    <Form.Label>Tag a Book?</Form.Label>
                    <Form.Control as="select" onChange={
                        (event) => {
                          const copy = {...userBookObject}
                          copy.title = event.target.options[event.target.selectedIndex].innerHTML
                          console.log(copy.title)
                          console.log(books.find(b => b.title === copy.title))
                          const selectedUserBookObject = books.find(b => b.title === copy.title)
                          console.log(selectedUserBookObject)
                          setUserBookObject(selectedUserBookObject)
                          console.log(userBookObject)
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
            <Button className="btn__btn-secondary" onClick={(e) => {
                    e.stopPropagation();
                    setShow(false)
                    }}>Close</Button>
            <Button className="btn__btn-primary" onClick={() => CreateNewPost()}>Post</Button>
        </Modal.Footer>
        </Modal>
        </Container>
    </> : null
  );
}