import React from "react";
import { Button, Col, Row } from "react-bootstrap";
import "./CurrentlyReading.css";

const CurrentlyReadingCard = ({userProfile, user, currentUser}) => {
    return (
        <div className="currently-reading-card">
            <Row>
                <Col>
                    <p>Book 1</p>
                </Col>
                <Col>
                    <p>Book 2</p>
                </Col>
                <Col>
                </Col>
                <Col>
                </Col>
            </Row>
        </div>
    );
};

export default CurrentlyReadingCard;