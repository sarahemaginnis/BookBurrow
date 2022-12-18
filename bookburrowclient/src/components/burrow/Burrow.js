import React, { useState } from "react";
import "./Burrow.css";
import { Card, Col, Row } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

export default function  BurrowPostGrid({user, posts}) {
    const navigate = useNavigate();

    // const navigateToPost = () => {
    //     (posts.map((post) => (navigate(`/post/${post.userPost.id}`))))
    // }

    return (
    <Row xs={1} md={3} className="g-3">
      {posts.length > 0 ? (posts.map((post) => {
        const navigateToPost = () => {
          navigate(`/post/${post.userPost.id}`)
        }
        return (
      <Col>
        <Card className="burrow-post-card" onClick={navigateToPost} style={{cursor: "pointer"}}>
          <Card.Img variant="top" src={post.userPost ? post.userPost.cloudinaryUrl : null} />
          <Card.Body>
            <Card.Title>{post.userPost ? post.userPost.title : null}</Card.Title>
            <Card.Text>{post.userPost ? post.userPost.caption : null}</Card.Text>
          </Card.Body>
          <Card.Footer></Card.Footer>
        </Card>
      </Col>
      )})
      ) : (<div></div>)}
    </Row>
  );
};