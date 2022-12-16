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
    
    console.log(user);
    console.log(currentUser);
    console.log(currentUser.id);

    //Get userProfile biography information from API and update state when the value of userProfileId changes
    useEffect(() => {
        console.log(userProfileId);
        fetch(`https://localhost:7210/api/UserProfileViewModel/UserBiography/${userProfileId}`, {
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

    //Get all userPosts
    useEffect(() => {
        console.log(currentUser.id);
        console.log(userProfile);
        if(userProfile.user.id){
            fetch(`https://localhost:7210/api/UserProfileViewModel/UserPosts/${userProfile.user.id}`, {
                method: "GET",
                headers: {
                    "Access-Control-Allow-Origin": "https://localhost:7210",
                    "Content-Type": "application/json",
                },
            })
            .then((res) => res.json())
            .then((data) => {
                syncPosts(data);
            });
        }
    }, [userProfile.user.id]);

    console.log(posts); //posts is not updating
   
  return ( userProfile.hasOwnProperty("id") ? 
    <>
      <Container>
        <BiographyCard userProfile={userProfile} user={user} currentUser={currentUser} />
        <Row>
            <Col>
                <h1>{userProfile.userProfile.handle} Bookshelves</h1>
            </Col>
            <Col>
                <img src={bookshelf} />
            </Col>
        </Row>
        <BookshelfCard userProfile={userProfile}/>
        <Row>
            <h1>{userProfile.userProfile.handle} is currently reading</h1>
        </Row>
        <CurrentlyReadingCard />
        <Row>
            <h1>Burrow</h1>
            {posts.length > 0 ? (
                posts.map((post) => (
                    <BurrowPostGrid user={user} post={post} key={post.id} />
                    )) 
                ) : (<div></div>)}
        </Row>
      </Container>
    </> : null
  );
};