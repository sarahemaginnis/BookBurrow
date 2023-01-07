import React, { useState, useEffect } from "react";
import { Container, Row, Col } from "react-bootstrap";
import { IoHeartSharp, IoPricetagsOutline } from "react-icons/io5";
import { GiMagicLamp } from "react-icons/gi";
import "./Authenticated.css";

export default function Authenticated({ user, currentUser }) {
  const [books, syncBooks] = useState([]);
  const [userProfileObject, setUserProfileObject] = useState({});
  const [userBookObject, setUserBookObject] = useState({});

  console.log(currentUser);

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
      });
  }, []);

  //Get userProfile information from API
  const GetUserProfile = () => {
    fetch(
      `https://localhost:7210/api/UserProfile/UserProfileByUserId/${currentUser.id}`,
      {
        method: "GET",
        headers: {
          "Access-Control-Allow-Origin": "https://localhost:7210",
          "Content-Type": "application/json",
        },
      }
    )
      .then((res) => res.json())
      .then((profile) => {
        setUserProfileObject(profile);
        console.log(userProfileObject);
      });
  };

  useEffect(() => {
    if (currentUser.hasOwnProperty("id")) {
      GetUserProfile();
      console.log("getting User Profile");
    }
  }, [currentUser]);

  return currentUser.hasOwnProperty("id") &&
    userProfileObject.hasOwnProperty("userId") ? (
    <>
      <Container>
        <Row>
          <Col sm={9}>
            <Row>
              <Col>
                <h4>
                  Following <IoHeartSharp />
                </h4>
              </Col>
              <Col>
                <h4>
                  For you <GiMagicLamp />
                </h4>
              </Col>
              <Col>
                <h4>
                  Your tags <IoPricetagsOutline />
                </h4>
              </Col>
            </Row>
            <Row>
              <h2>Post Cards Go Here!</h2>
            </Row>
          </Col>
          <Col sm={3}>
            <h3>Currently Reading Book Cards go below!</h3>
          </Col>
        </Row>
      </Container>
    </>
  ) : null;
}
