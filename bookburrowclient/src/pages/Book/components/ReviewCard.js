import React from "react";
import { Button, Col, Row } from "react-bootstrap";
import { BsPencilFill } from "react-icons/bs";
import "./ReviewCard.css";

const ReviewCard = ({book, user, currentUser, userBook, userProfile}) => {

    return(
        <div className="review-card">
            <Row>
                <Col sm={4}>
                </Col>
                <Col sm={8}>
                    <h3>Friends & Following</h3>
                    <p>Need an if statement - if no friends or following reviews then display "No one you know has read this book. Recommend it to a friend!"</p>
                    <h3>Community Reviews</h3>
                    <Row>
                        <Col>
                            <p>Average star rating</p>
                        </Col>
                        <Col>
                            <p>Average numerical rating</p>
                        </Col>
                        <Col>
                            <p>Total numerical number of ratings(e.g., 80,710 ratings)</p>
                        </Col>
                        <Col>
                            <p>Total numerical number of reviews(e.g, 15,749 reviews)</p>
                        </Col>
                        <p>5 stars</p>
                        <p>4 stars</p>
                        <p>3 stars</p>
                        <p>2 stars</p>
                        <p>1 stars</p>
                        <p>Search review text</p>
                        <p>Displaying 1-10 of total reviews (e.g., 814 reviews)</p>
                        <p>Insert user review card</p>
                    </Row>
                </Col>
            </Row>
        </div>
    );
};
export default ReviewCard;

