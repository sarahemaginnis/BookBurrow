import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { Button, Col, Container, Row } from "react-bootstrap";
import "./Biography.css";

const BiographyCard = ({userProfile, user, currentUser, userPronoun, userFollower, setUserFollower, userProfileId, userFollowerObject, setUserFollowerObject}) => {
    const [userPostCount, setUserPostCount] = useState(0); //initial state variable for current userPostCount object
    const [userFollowerCount, setUserFollowerCount] = useState(0); //initial state variable for current userFollowerCount object
    const [userFollowingCount, setUserFollowingCount] = useState(0); //initial state variable for current userFollowingCount object
    //const [profileSet, setProfile] = useState(false);

    const navigation = useNavigate();
  
    const navigateToEditProfile = () => {
      navigation(`/user/settings/${userProfile.id}`)
    }

    const navigateToProfile = () => {
        navigation(`/user/${userProfileId}`)
    }

    //Get userPostCount information from API and update state when the value of userPostCount changes
    useEffect(() => {
        console.log(user.id);
            fetch(`https://localhost:7210/api/UserProfileViewModel/UserPostCount/${user.id}`, {
                method: "GET",
                headers: {
                "Access-Control-Allow-Origin": "https://localhost:7210",
                "Content-Type": "application/json",
                },
            })
            .then((res) => res.json())
            .then((data) => {
                setUserPostCount(data);
            });
    }, [userPostCount, userProfile]);

        //Get userFollowercount information from API and update state when the value of userFollowerCount changes
        useEffect(() => {
            console.log(user.id);
                fetch(`https://localhost:7210/api/UserProfileViewModel/UserFollowerCount/${user.id}`, {
                    method: "GET",
                    headers: {
                    "Access-Control-Allow-Origin": "https://localhost:7210",
                    "Content-Type": "application/json",
                    },
                })
                .then((res) => res.json())
                .then((data) => {
                    setUserFollowerCount(data);
                });
        }, [userFollowerCount, userProfile, userFollower]);
    
            //Get userFollowingCount information from API and update state when the value of userFollowingCount changes
            useEffect(() => {
                console.log(user.id);
                    fetch(`https://localhost:7210/api/UserProfileViewModel/UsersFollowingCount/${user.id}`, {
                        method: "GET",
                        headers: {
                        "Access-Control-Allow-Origin": "https://localhost:7210",
                        "Content-Type": "application/json",
                        },
                    })
                    .then((res) => res.json())
                    .then((data) => {
                        setUserFollowingCount(data);
                    });
            }, [userFollowingCount, userProfile, userFollowerObject]);

//Follow User (POST)
const CreateNewUserFollower = () => {
    const newUserFollowerObject = {
        userId: userProfileId,
        followerId: currentUser.id
    }
    const fetchOptions = {
        method: 'POST',
        headers: {
            "Access-Control-Allow-Origin": "https://localhost:7210",
            "Content-Type": "application/json",
        },
        body: JSON.stringify(newUserFollowerObject)
    }
    return fetch('https://localhost:7210/api/UserFollower', fetchOptions)
    .then(res => res.json())
    .then((data) => {
        setUserFollowerObject(data)
    })
    .then(setUserFollower(true))
    .then(navigateToProfile)
}

console.log(userFollowerObject);

//Stop Following User (DELETE)
const DeleteFollow = (id) => {
    fetch(`https://localhost:7210/api/UserFollower/${id}`, {
            method: "DELETE"
        })
        .then(setUserFollower(false))
        .then(navigateToProfile)
}

console.log(userFollower);

    const profileButton = () => {
        if (currentUser.id === userProfile.userId){
            return <Button className="btn-primary" onClick={navigateToEditProfile}>Edit profile</Button>
        } else if (userFollower === false){
            return <Button className="btn-primary" onClick={() => CreateNewUserFollower()}>Follow</Button>
    } else {
        return <Button className="btn-primary" onClick={() => DeleteFollow(userFollowerObject.id)}>Following</Button>
    }
}
    
    return (
        <div className="biography-card">
            <Row>
                <Col sm={4}>
                    <img src={`${userProfile.id ? userProfile.profileImageUrl : null}`} />
                </Col>
                <Col sm={8}>
                    <Row>
                        <Col>
                            <h2 className="biography-card-handle">{userProfile.id ? userProfile.handle : null}</h2>
                        </Col>
                        <Col>
                        {profileButton()}
                        </Col>
                    </Row>
                    <Row>
                        <Col>
                            <p className="biography-card-total-posts">{userPostCount} posts</p>
                        </Col>
                        <Col>
                            <p className="biography-card-total-followers">{userFollowerCount} followers</p>
                        </Col>
                        <Col>
                            <p className="biography-card-total-following">{userFollowingCount} following</p>
                        </Col>
                    </Row>
                    <Row>
                        <Col>
                            <p className="biography-card-name">{userProfile.id ? userProfile.firstName : null} {userProfile.id ? userProfile.lastName : null}</p>
                        </Col>
                        <Col>
                            <p className="biography-card-pronouns">{userPronoun ? userPronoun.pronouns : ""}</p>
                        </Col>
                    </Row>
                    <Row>
                        <Col>
                            <p className="biography-card-biography">{userProfile.id ? userProfile.biography : null}</p>
                        </Col>
                    </Row>
                    <Row>
                        <Col>
                            <p className="biography-card-biography-url">{userProfile.id ? userProfile.biographyUrl : null}</p>
                        </Col>
                    </Row>
                </Col>
            </Row>
        </div>
    );
};

export default BiographyCard;