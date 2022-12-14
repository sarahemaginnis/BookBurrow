import React from "react";
import { Button, Col, Row } from "react-bootstrap";
import "./BookshelfCard.css";

const BookshelfCard = ({userProfile, user, currentUser}) => {
    return (
        <div className="bookshelf-card">
            <Row>
                <Col>
                    <p>Shelf 1 (total number)</p>
                </Col>
                <Col>
                    <p>Shelf 2 (total number)</p>
                </Col>
                <Col>
                    <p>Shelf 3 (total number)</p>
                </Col>
                <Col>
                    <p>Shelf 4 (total number)</p>
                </Col>
            </Row>
        </div>
    );
};

export default BookshelfCard;