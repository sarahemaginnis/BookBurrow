import React from "react";
import { Button, Col, Row } from "react-bootstrap";
import "./Biography.css";

const BiographyCard = ({userProfile, user, currentUser}) => {
    
    // const profileButton = () => {
    //     if (user === currentUser){
    //         <Button className="btn-primary">Edit profile</Button>
    //     } else if (user === following){
    //         <Button className="btn-primary">Following</Button>
    //     } else {
    //         <Button className="btn-primary">Follow</Button>
    //     }
    // }
    
    return (
        <div className="biography-card">
            <Row>
                <Col sm={4}>
                    <img src={`${userProfile.profileImageUrl}`} />
                </Col>
                <Col sm={8}>
                    <Row>
                        <Col>
                            <h2 className="biography-card-handle">{userProfile.handle}</h2>
                        </Col>
                        <Col>
                            <Button className="btn-primary">Edit profile/follow/following</Button>
                        </Col>
                    </Row>
                    <Row>
                        <Col>
                            <p className="biography-card-total-posts">Total number of posts</p>
                        </Col>
                        <Col>
                            <p className="biography-card-total-followers">Total number of followers</p>
                        </Col>
                        <Col>
                            <p className="biography-card-total-following">Total number following</p>
                        </Col>
                    </Row>
                    <Row>
                        <Col>
                            <p className="biography-card-name">{userProfile.firstName} {userProfile.lastName}</p>
                        </Col>
                        <Col>
                            <p className="biography-card-pronouns">{userProfile.userPronoun.pronouns}</p>
                        </Col>
                    </Row>
                    <Row>
                        <Col>
                            <p className="biography-card-biography">{userProfile.biography}</p>
                        </Col>
                    </Row>
                    <Row>
                        <Col>
                            <p className="biography-card-biography-url">{userProfile.biographyUrl}</p>
                        </Col>
                    </Row>
                </Col>
            </Row>
        </div>
    );
};

export default BiographyCard;