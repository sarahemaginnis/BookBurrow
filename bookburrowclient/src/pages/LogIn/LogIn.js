import React from "react";
import { Container, Row, Col } from "react-bootstrap";
import { signIntoFirebase } from "../../utils/auth";
import MyImage from '.BookBurrowLogo.png';

export default function LogIn() {
  return (
    <div className="text-center mt-5">
      <Container>
        <Row>
          <Col>
            <h1>Book Burrow</h1>
            <h2>Find your fandom</h2>
            <h2>find your friends.</h2>
            <button
              type="button"
              className="btn btn-success"
              onClick={signIntoFirebase}
            >
              Enter
            </button>
          </Col>
          <Col>
            <div>
              <img className="image__login" src={MyImage} alt="Book Burrow Logo" />
            </div>
          </Col>
        </Row>
      </Container>
    </div>
  );
}
