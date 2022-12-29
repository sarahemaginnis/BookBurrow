import React, { useState, useEffect } from "react";
import { Button, Col, Row, Card } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import "./BurrowCard.css";


const BurrowCard = ({bookId, book, user, currentUser, userProfile, bookStatusOptions}) => {
    const [posts, setPosts] = useState([]); //State variable for array of posts

    const navigate = useNavigate();
    console.log(book)

    //Fetch all bookPosts (get UserBooks by Bookid)
    useEffect(() => {
        fetch (`https://localhost:7210/api/BookViewModel/GetBookPosts/${bookId}`, {
            method: "GET",
            headers: {
                "Access-Control-Allow-Origin": "https://localhost:7210",
                "Content-Type": "application/json",
            },
        })
        .then((res) => res.json())
        .then((data) => {
            setPosts(data)
            console.log("setting posts", posts)
        })
    }, []); 

    //map through book posts and then pass in props to Burrow: user, post

    return (
        <div className="burrow-card">
            <Row>
                <Col sm={4}>
                </Col>
                <Col sm={8}>
                    <Row>
                        <h1>Burrow</h1>
                    </Row>
                    <Row>
                        <Col>
                            <h3>Latest</h3>
                        </Col>
                        <Col>
                            <h3>Top</h3>
                        </Col>
                    </Row>
                    <div>
                    <Row xs={1} md={3} className="g-3">
      {posts.length > 0 ? (posts.map((post) => {
        const navigateToPost = () => {
          navigate(`/post/${post.id}`)
        }
        return (
      <Col>
        <Card className="burrow-post-card" onClick={navigateToPost} style={{cursor: "pointer"}}>
          <Card.Img variant="top" src={post.id ? post.cloudinaryUrl : null} />
          <Card.Body>
            <Card.Title>{post.id ? post.title : null}</Card.Title>
            <Card.Text>{post.id ? post.caption : null}</Card.Text>
          </Card.Body>
          <Card.Footer></Card.Footer>
        </Card>
      </Col>
      )})
      ) : (<div></div>)}
    </Row>
                    </div>
                    {/*<BurrowPostGrid user={user} post={post} /> */}
                </Col>
            </Row>
        </div>
    );
};
export default BurrowCard;