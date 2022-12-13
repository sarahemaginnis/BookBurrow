import React from "react";
import "./Navbar.css";
import {BsFillHouseDoorFill, BsFillEnvelopeFill, BsFillBellFill, BsFillPersonFill, BsPencilFill } from "react-icons/bs";
import {ImCompass2} from "react-icons/im";
import { signOutOfFirebase } from "../../utils/auth";
import { Nav, NavItem, NavLink, NavDropdown, Button, Container, Navbar, Form } from 'react-bootstrap';
import { useNavigate } from "react-router";
import logo from './BookBurrowLogo.png';

export const NavBar = ({ user }) => {
  const navigation = useNavigate();
  
  const navigateToDashboard = () => {
    navigation('/dashboard')
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
    navigation('/${user}')
  }

  const navigateToMakePost = () => {
    navigation('/new')
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
              <Nav.Link onClick={navigateToMakePost}><BsPencilFill /></Nav.Link>
            </Nav>
          </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};

export default NavBar;
