import './LogIn.css';
import React from "react";
import { Container, Row, Col, Button } from "react-bootstrap";
import { signIntoFirebase } from "../../utils/auth";
import MyImage from "./BookBurrowLogo.png";

export default function LogIn() {
  return (
    <div className="text-center mt-5">
      <Container>
          <Row>
            <Col>
            <Row>
              <h1>Book Burrow</h1>
            </Row>
            <Row>
              <h2>Find your fandom</h2>
              <h2>find your friends.</h2>
            </Row>
              <Button
                type="button"
                className="btn__btn-primary"
                onClick={signIntoFirebase}
              >
                Enter
              </Button>
            </Col>
            <Col>
              <div>
                <img
                  className="image__login"
                  src={MyImage}
                  alt="Book Burrow Logo"
                />
              </div>
            </Col>
          </Row>
      </Container>
    </div>
  );
}
