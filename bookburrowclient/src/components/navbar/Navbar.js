import React, {useState, useEffect} from "react";
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

export const NavBar = ({ user }) => {
  const [userProfileObject, setUserProfileObject] = useState({});
  const [currentUser, setCurrentUser] = useState({});
  const [show, setShow] = useState(false);

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
              placeholder="Search"
              className="me-2"
              aria-label="Search"
              />
              <Button variant="outline-success">Search</Button>
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
      <Modal show={show} onHide={handleClose} size="lg" centered className="modal__new">
        <Modal.Header className="modal__header" closeButton></Modal.Header>
        <Modal.Body className="modal__body">
          <Container>
              <Row>
                  <Col><GoTextSize onClick={() => {<CreatePost user={user} currentUser={currentUser} userProfile={userProfileObject} show={handleShow} />}} /></Col>
                  <Col><AiOutlineCamera /></Col>
                  <Col><ImQuotesLeft /></Col>
                  <Col><ImLink /></Col>
                  <Col><HiChatBubbleLeftRight /></Col>
                  <Col><FaHeadphonesAlt /></Col>
                  <Col><BsCameraVideoFill /></Col>
              </Row>
          </Container>
        </Modal.Body>
        <Modal.Footer className="modal__footer"></Modal.Footer>
    </Modal>
    </Navbar>
  );
};

export default NavBar;
