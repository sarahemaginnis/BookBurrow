import React, { useEffect, useState } from "react";
import { useParams } from "react-router";
import { Container, Row, Col } from "react-bootstrap";
import './UserProfile.css';
import BiographyCard from "./components/Biography/BiographyCard";
import bookshelf from "./Bookshelf.png";
import BookshelfCard from "./components/Bookshelf/BookshelfCard";
import CurrentlyReadingCard from "./components/CurrentlyReading/CurrentlyReadingCard";
import BurrowPostGrid from "../../components/burrow/Burrow";

export default function UserProfile ({user, currentUser}) {
    const [userProfile, setUserProfile] = useState({}); //initial state variable for current userProfile object
    const {userProfileId} = useParams(); //variable storing the route parameter
    const [posts, syncPosts] = useState([]); //State variable for array of posts

    //Get book detail information from API and update state when the value of userProfileId changes
    useEffect(() => {
        console.log(userProfileId);
        fetch(`https://localhost:7210/api/UserProfile/${userProfileId}`, {
            method: "GET",
            headers: {
              "Access-Control-Allow-Origin": "https://localhost:7210",
              "Content-Type": "application/json",
            },
        })
        .then((res) => res.json())
        .then((data) => {
            setUserProfile(data);
        });
    }, [userProfileId]);

    //TO-DO: Fetch all userPosts
    //TO-DO: map through book posts and then pass in props to Burrow: user, post

    //TO-DO: pass down userProfile object and user and currentUser object into components to render properly and pass into fetch calls    
  return ( userProfile.hasOwnProperty("id") ? 
    <>
      <Container>
        <BiographyCard userProfile={userProfile} user={user} currentUser={currentUser} />
        <Row>
            <Col>
                <h1>{userProfile.handle} Bookshelves</h1>
            </Col>
            <Col>
                <img src={bookshelf} />
            </Col>
        </Row>
        <BookshelfCard userProfile={userProfile}/>
        <Row>
            <h1>{userProfile.handle} is currently reading</h1>
        </Row>
        <CurrentlyReadingCard />
        <Row>
            <h1>Burrow</h1>
            <BurrowPostGrid user={user} post={post}/>
        </Row>
      </Container>
    </> : null
  );
};