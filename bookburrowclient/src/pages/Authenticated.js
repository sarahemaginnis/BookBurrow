import React from 'react';
import { signIntoFirebase, signOutOfFirebase } from '../utils/auth';
import {Container, Row, Col} from "react-bootstrap";
import {IoHeartSharp, IoPricetagsOutline} from "react-icons/io5";
import {GiMagicLamp} from "react-icons/gi";
import {GoTextSize} from "react-icons/go";
import {AiOutlineCamera} from "react-icons/ai";
import {HiChatBubbleLeftRight} from "react-icons/hi2";
import {FaHeadphonesAlt} from "react-icons/fa";
import {BsCameraVideoFill } from "react-icons/bs";
import {ImQuotesLeft, ImLink} from "react-icons/im";

export default function Authenticated({ user, currentUser }) {
  console.log(currentUser);
    const buttonClick = () => {
        if (user){
            signOutOfFirebase()
        } else {
            signIntoFirebase()
        }
    }

  return (
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
                <Col><GoTextSize /></Col>
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
      <h1>Welcome, {user.displayName}!</h1>
      <img
        referrerPolicy="no-referrer"
        src={user.photoURL}
        alt={user.displayName}
        />
      <h1>{currentUser.name}</h1>
      <h5>{currentUser.email}</h5>
      <div className="mt-2">
        <button type="button" className="btn btn-danger" onClick={buttonClick}>
          {user ? "Sign Out" : "Sign In"}
        </button>
      </div>
    </div>
        </Container>
  );
}