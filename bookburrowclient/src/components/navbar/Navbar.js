import React, {useState, useEffect} from "react";
import Select from "react-select";
import "./Navbar.css";
import {BsFillHouseDoorFill, BsFillEnvelopeFill, BsFillBellFill, BsFillPersonFill, BsPencilFill, BsCameraVideoFill } from "react-icons/bs";
import {ImCompass2, ImQuotesLeft, ImLink} from "react-icons/im";
import { signOutOfFirebase } from "../../utils/auth";
import { Nav, NavItem, NavLink, NavDropdown, Button, Container, Navbar, Form, Col, Row, Modal } from 'react-bootstrap';
import logo from './BookBurrowLogo.png';
import { useNavigate } from "react-router-dom";
import {GoTextSize} from "react-icons/go";
import {AiOutlineCamera} from "react-icons/ai";
import {HiChatBubbleLeftRight} from "react-icons/hi2";
import {FaHeadphonesAlt} from "react-icons/fa";
import CreatePost from "../newPost/NewPost";
import UploadWidget from "../UploadWidget";

export const NavBar = ({ user }) => {
  const [userProfileObject, setUserProfileObject] = useState({});
  const [books, syncBooks] = useState([]);
  const [currentUser, setCurrentUser] = useState({});
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
  const [modalView, setModalView] = useState(1);
  const [show, setShow] = useState(false);
  const [selectedBookId, setSelectedBookId] = useState("");

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  useEffect(() => {
    GetUser(user);
  }, []);

  const GetUser = () => {
    fetch (`https://localhost:7210/api/LoginViewModel/VerifyUser/${user.uid}`, {
      method: "GET",
      headers: {
        "Access-Control-Allow-Origin": "https://localhost:7210",
        "Content-Type": "application/json",
      },
    })
      .then((res) => (res.status === 200 ? res.json() : ""))
      .then((r) => {
        setCurrentUser(r);
      });
  };

  //Get userProfile information from API
  const GetUserProfile = () => {
    fetch (`https://localhost:7210/api/UserProfile/UserProfileByUserId/${currentUser.id}`, {
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
    if(currentUser.hasOwnProperty("id")){GetUserProfile() ; console.log("getting userProfile")}
  }, [currentUser]);

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

    //Function that gets passed down to UploadWidget
    const pullData = (data) => {
      console.log(data);
      setPostCloudinaryUrl(data);
      console.log(postCloudinaryUrl)
  }
  
  const navigation = useNavigate();
  
  const navigateToDashboard = () => {
    navigation('/')
  }

  const navigateToExplore = () => {
    navigation('/explore')
  }

  const navigateToInbox = () => {
    navigation('/inbox')
  }

  const navigateToNotifications = () => {
    navigation('/${user}/activity')
  }

  const navigateToProfile = () => {
    navigation(`/user/${userProfileObject.userId}`)
  }

  const navigateToSearch = () => {
    navigation('/search')
  }

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
.then(setPostCloudinaryUrl(""))
.then(handleClose)
.then(setModalView(1))
}


  const modalViewSwitch = () => {
    if(modalView === 1) {
      return <div>
        <Modal show={show} onHide={handleClose} size="lg" centered className="modal__new">
        <Modal.Header className="modal__header" closeButton></Modal.Header>
        <Modal.Body className="modal__body">
          <Container>
              <Row>
                  <Col><GoTextSize onClick={(e) => {
                    e.stopPropagation();
                    setModalView(2);
                    setPostTypeName("Text");
                    setPostTypeValue(0);
                  }} /></Col>
                  <Col><AiOutlineCamera onClick={(e) => {
                    e.stopPropagation();
                    setModalView(3);
                    setPostTypeName("Photo");
                    setPostTypeValue(1);
                  }} /></Col>
                  <Col><ImQuotesLeft onClick={(e) => {
                    e.stopPropagation();
                    setPostTypeName("Quote");
                    setPostTypeValue(2);
                  }} /></Col>
                  <Col><ImLink onClick={(e) => {
                    e.stopPropagation();
                    setPostTypeName("Link");
                    setPostTypeValue(3);
                  }}/></Col>
                  <Col><HiChatBubbleLeftRight onClick={(e) => {
                    e.stopPropagation();
                    setPostTypeName("Chat");
                    setPostTypeValue(4);
                  }}/></Col>
                  <Col><FaHeadphonesAlt onClick={(e) => {
                    e.stopPropagation();
                    setPostTypeName("Audio");
                    setPostTypeValue(5);
                  }} /></Col>
                  <Col><BsCameraVideoFill onClick={(e) => {
                    e.stopPropagation();
                    setPostTypeName("Video");
                    setPostTypeValue(6);
                  }} /></Col>
              </Row>
          </Container>
        </Modal.Body>
        <Modal.Footer className="modal__footer"></Modal.Footer>
    </Modal>
      </div>
    } else if(modalView === 2) { 
      return <div>
        <Modal show={show} onHide={() => {
          handleClose()
          setModalView(1)}} size="lg" centered className="modal__new">
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
                    <Select onChange={(event) => {
                      const selectedUserBookObject = books.find(b => b.id === event.value)
                      console.log(selectedUserBookObject)
                      setUserBookObject(selectedUserBookObject)
                      console.log(userBookObject)
                      setSelectedBookId(event.value)
                    }} 
                    options={books.map((e) => ({
                      value: e.id, 
                      label: e.title
                    }))} />
                    </Form.Group>
                </Col>
              </Row>
            </Form>
          </Container>
        </Modal.Body>
        <Modal.Footer className="modal__footer">
            <Button className="btn__btn-secondary" onClick={(e) => {
                    e.stopPropagation();
                    setShow(false);
                    setModalView(1);
                    }}>Close</Button>
            <Button className="btn__btn-primary" onClick={() => CreateNewPost()}>Post</Button>
        </Modal.Footer>
        </Modal>
      </div>
    } else if(modalView === 3) {
      return <div>
        <Modal show={show} onHide={() => {
          handleClose()
          setModalView(1)
        }} size="lg" centered className="modal_new">
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
                  <UploadWidget func={pullData} />
                  </Col>
                </Row>
                <Row>
                  <Col>
                    <img src={`${userProfileObject.id ? postCloudinaryUrl : null}`} />
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
                    <Select onChange={(event) => {
                      const selectedUserBookObject = books.find(b => b.id === event.value)
                      console.log(selectedUserBookObject)
                      setUserBookObject(selectedUserBookObject)
                      console.log(userBookObject)
                      setSelectedBookId(event.value)
                    }} 
                    options={books.map((e) => ({
                      value: e.id, 
                      label: e.title
                    }))} />
                    </Form.Group>
                </Col>
              </Row>
              </Form>
            </Container>
          </Modal.Body>
          <Modal.Footer className="modal__footer">
            <Button className="btn__btn-secondary" onClick={(e) => {
                    e.stopPropagation();
                    setShow(false);
                    setModalView(1);
                    }}>Close</Button>
            <Button className="btn__btn-primary" onClick={() => CreateNewPost()}>Post</Button>
        </Modal.Footer>
        </Modal>
      </div>
    }
  }

  return (
    <Navbar bg="#f6f2e9" expand="lg" className="navbar">
      <Container className="navbar__border">
        <Navbar.Brand onClick={navigateToDashboard}>
          <img src={logo}
          alt="Book Burrow"
          className="navbar__logo"
          />
          </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
          <Form className="d-flex">
            <Form.Control type="search"
              placeholder="Search Book Burrow"
              className="me-2"
              aria-label="Search"
              />
              <Button onClick={navigateToSearch}>Search</Button>
          </Form>
            <Nav className="ms-auto">
              <Nav.Link onClick={navigateToDashboard}><BsFillHouseDoorFill /></Nav.Link>
              <Nav.Link onClick={navigateToExplore}><ImCompass2 /></Nav.Link>
              <Nav.Link onClick={navigateToInbox}><BsFillEnvelopeFill /></Nav.Link>
              <Nav.Link onClick={navigateToNotifications}><BsFillBellFill /></Nav.Link>
              <NavDropdown title={<BsFillPersonFill/>} id="basic-nav-dropdown">
                <NavDropdown.Item onClick={navigateToProfile}>Profile</NavDropdown.Item>
                <NavDropdown.Divider />
                <NavDropdown.Item onClick={signOutOfFirebase}>Sign Out</NavDropdown.Item>
              </NavDropdown>
              <Nav.Link><BsPencilFill onClick={(e) => {
                    e.stopPropagation();
                    setShow(true)
                    }} /></Nav.Link>
            </Nav>
          </Navbar.Collapse>
      </Container>
        {modalViewSwitch()}
    </Navbar>
  );
};

export default NavBar;
