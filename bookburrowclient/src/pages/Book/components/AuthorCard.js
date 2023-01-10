import React from "react";
import { Button, Col, Row } from "react-bootstrap";
import "./AuthorCard.css";

const AuthorCard = ({book, user, currentUser, userProfile, bookStatusOptions}) => {

    return (
        <div className="author-card">
            <Row>
                    <h2>About the Author</h2>
                    <Row>
                        <Col>
                            <img src={`${book.bookAuthor.author.profileImageUrl}`} />
                        </Col>
                        <Col>
                            <h3>{book.bookAuthor.author.firstName} {book.bookAuthor.author.middleName} {book.bookAuthor.author.lastName}</h3>
                        <Row>
                            <p>Author biography goes here</p>
                        </Row>
                        </Col>
                    </Row>
                    </Row>
        </div>
    );
};
export default AuthorCard;