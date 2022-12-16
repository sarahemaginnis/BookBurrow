import React, { useState } from "react";
import "./Burrow.css";
import { Card, Col, Row } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

export default function  BurrowPostGrid({user, post, key}) {
    const navigate = useNavigate();

    const navigateToPost = () => {
        navigate(`/post/${post.id}`)
    }
  
    return (
    <Row xs={1} md={3} className="g-3">
        <Col>
          <Card className="burrow-post-card" onClick={navigateToPost} style={{cursor: "pointer"}}>
            <Card.Img variant="top" src={post.cloudinaryUrl} />
            <Card.Body>
              <Card.Title>{post.title}</Card.Title>
              <Card.Text>{post.caption}</Card.Text>
            </Card.Body>
            <Card.Footer></Card.Footer>
          </Card>
        </Col>
    </Row>
  );
};