import React, { useEffect, useState } from "react";
import { useParams } from "react-router";
import { Container, Row, Card, Col } from "react-bootstrap";
import {RiShareForwardLine} from "react-icons/ri";
import {FaRegComment, FaRegBookmark} from "react-icons/fa";
import {BiRepost} from "react-icons/bi";
import {BsSuitHeart} from "react-icons/bs";
import './Post.css';

export default function PostPage ({user, currentUser}) {
    const [post, setPost] = useState({}); //initial state variable for current book object
    const {postId} = useParams(); //variable storing the route parameter

    //Get post detail information from API and update state when the value of postId changes
    useEffect(() => {
        console.log(postId);
        fetch(`https://localhost:7210/api/UserPost/${postId}`, {
            method: "GET",
            headers: {
              "Access-Control-Allow-Origin": "https://localhost:7210",
              "Content-Type": "application/json",
            },
        })
        .then((res) => res.json())
        .then((data) => {
            setPost(data);
        });
    }, [postId]);

//pass down post object and user and currentUser object into components to render properly and pass into fetch calls    
  return ( post.hasOwnProperty("id") ? 
    <>
    <Container>
    <Row>
    <Col>
    <Card>
        <Card.Header>
            <Container>
                <Row>
                    <Col><img src={post.userProfile.profileImageUrl} /></Col>
                    <Col><p>{post.userProfile.handle}</p></Col>
                    <Col><p>Follow/Following</p></Col>
                </Row>
            </Container>
        </Card.Header>
        <Card.Img variant="top" src={post.cloudinaryUrl} />
        <Card.Body>
            <Card.Title>{post.title}</Card.Title>
            <Card.Text>
                <p>{post.caption}</p>
                <p>{post.source}</p>
            </Card.Text>
        </Card.Body>
        <Card.Footer>
            <Container>
                <Row>
                    <Col>
                        <p>total number of notes</p>
                    </Col>
                    <Col><RiShareForwardLine /></Col>
                    <Col><FaRegComment /></Col>
                    <Col><BiRepost /></Col>
                    <Col><BsSuitHeart /></Col>
                    <Col><FaRegBookmark /></Col>
                </Row>
                <Row>
                    <p>Post comments go here!</p>
                </Row>
            </Container>
        </Card.Footer>
    </Card>
    </Col>
    </Row>
    </Container>
    </> : null
  );
};