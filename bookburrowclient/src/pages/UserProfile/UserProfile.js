import React, { useEffect, useState } from "react";
import { useParams } from "react-router";
import { Container, Row, Col } from "react-bootstrap";
import "./UserProfile.css";
import BiographyCard from "./components/Biography/BiographyCard";
import BookshelfCard from "./components/Bookshelf/BookshelfCard";
import CurrentlyReadingCard from "./components/CurrentlyReading/CurrentlyReadingCard";
import BurrowPostGrid from "../../components/burrow/Burrow";

export default function UserProfile({
  user,
  currentUser,
  bookAuthors
}) {
  const [userProfileObject, setUserProfile] = useState({}); //initial state variable for current userProfile object
  const [userObject, setUser] = useState({}); //initial state variable for current user object
  const [userPronounObject, setUserPronoun] = useState({}); //initial state variable for current userPronoun Object
  const [userBooks, setUserBooks] = useState([]); //initial state variable for array of current userProfile userBooks
  const { userProfileId } = useParams(); //variable storing the route parameter
  const [posts, setPosts] = useState([]); //State variable for array of posts
  const [userFollower, setUserFollower] = useState(false);
  const [userFollowerObject, setUserFollowerObject] = useState({});

  const GetUserPosts = () => {
    fetch(
      `https://localhost:7210/api/UserProfileViewModel/UserPosts/${userObject.id}`,
      {
        method: "GET",
        headers: {
          "Access-Control-Allow-Origin": "https://localhost:7210",
          "Content-Type": "application/json",
        },
      }
    )
      .then((res) => res.json())
      .then((data) => {
        setPosts(data);
        console.log("setting posts", posts);
      });
  };

  //Get userProfile biography information from API and update state when the value of userProfileId changes
  useEffect(() => {
    console.log(userProfileId);
    fetch(
      `https://localhost:7210/api/UserProfileViewModel/UserBiography/${userProfileId}`,
      {
        method: "GET",
        headers: {
          "Access-Control-Allow-Origin": "https://localhost:7210",
          "Content-Type": "application/json",
        },
      }
    )
      .then((res) => res.json())
      .then((data) => {
        console.log("setUserProfile");
        setUserProfile(data.userProfile);
        setUser(data.user);
        setUserPronoun(data.userPronoun);
        console.log("data below");
        console.log(data);
        console.log(userProfileObject);
      });
  }, []);

  useEffect(() => {
    if (userProfileObject.hasOwnProperty("id")) {
      GetUserPosts();
      console.log("getting User Posts");
    }
  }, [userProfileObject]);

    //get userBooks by userId
    useEffect(() => {
      console.log(userProfileId);
      fetch(`https://localhost:7210/api/UserBook/${userProfileId}`, {
        method: "GET",
        headers: {
          "Access-Control-Allow-Origin": "https://localhost:7210",
          "Content-Type": "application/json",
          },
        })
        .then((res) => res.json())
        .then((data) => {
          setUserBooks(data);
        })
    }, []);

    //verify userFollower status and set at page load
    useEffect(() => {
      if(currentUser.hasOwnProperty("id")){
        fetch(`https://localhost:7210/api/UserFollower/VerifyFollower?userId=${currentUser.id}&profileId=${userProfileId}`, {
          method: "GET",
          headers: {
            "Access-Control-Allow-Origin": "https://localhost:7210",
            "Content-Type": "application/json",
            },
          })
          .then((res) => res.json())
          .then((data) => {
            setUserFollowerObject(data)
            if(data.status === 404){
              return setUserFollower(false)
            } else {
              return setUserFollower(true)
            }
          })
      }
    }, [userProfileId, userFollower, currentUser]);

    console.log(userFollower);
    console.log(userFollowerObject);

  return userProfileObject.hasOwnProperty("id") ? (
    <>
      <Container>
        <BiographyCard
          userProfile={userProfileObject}
          user={userObject}
          currentUser={currentUser}
          userPronoun={userPronounObject}
          userFollower={userFollower}
          setUserFollower={setUserFollower}
          userProfileId={userProfileId}
          userFollowerObject={userFollowerObject}
          setUserFollowerObject={setUserFollowerObject}
        />
        {/* <Row>
          <Col>
            <h2 className="text-transform">Bookshelves</h2>
          </Col>
        </Row>
        <BookshelfCard userProfile={userProfileObject} /> */}
        <Row>
          <h2 className="text-transform">
            currently reading
          </h2>
          <div>
            <Col>
            {userBooks.length > 0 ? (
                <CurrentlyReadingCard
                userProfile={userProfileObject}
                user={userObject}
                currentUser={currentUser}
                userBooks={userBooks}
                bookAuthors={bookAuthors}
                />
                ) : (
                    <div></div>
                    )}
            </Col>
          </div>
        </Row>
        <Row >
          <h2 className="text-transform">Burrow</h2>
          <div>
            {posts.length > 0 ? (
              <BurrowPostGrid user={userProfileObject} posts={posts} />
            ) : (
              <div></div>
            )}
          </div>
        </Row>
      </Container>
    </>
  ) : null;
}