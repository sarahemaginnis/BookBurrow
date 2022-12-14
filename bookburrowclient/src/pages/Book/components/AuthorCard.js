import React from "react";
import { Button, Col, Row } from "react-bootstrap";
import "./AuthorCard.css";

const AuthorCard = ({book, user, currentUser}) => {

    return (
        <div className="author-card">
            <Row>
                <Col sm={4}>
                </Col>
                <Col sm={8}>
                    <h2>About the Author</h2>
                    <Row>
                        <Col>
                            <img src={`${book.author.profileImageUrl}`} />
                        </Col>
                        <Col>
                            <p>{book.author.firstName} {book.author.middleName} {book.author.lastName}</p>
                        <Row>
                            <Col>
                                <p>Total number of books on App (e.g., 20 books)</p>
                            </Col>
                            <Col>
                                <p>Total number of followers (e.g., 6,232 followers)</p>
                            </Col>
                            <Col>
                                <Button>Follow/Following</Button>
                            </Col>
                        </Row>
                        </Col>
                        <p>Author biography</p>
                    </Row>
                </Col>
            </Row>
        </div>
    );
};
export default AuthorCard;