import React, { useState, useEffect } from "react";
import { Button, Col, Row } from "react-bootstrap";
import "./CurrentlyReading.css";

const CurrentlyReadingCard = ({userProfile, user, currentUser}) => {
    const [books, syncBooks] = useState([]); //State variable for array of books
    //const [profileSet, setProfile] = useState(false);

    
    //Get all books currently reading
    useEffect(() => {
        console.log(user.id);
        fetch(`https://localhost:7210/api/api/UserProfileViewModel/UserBooksCurrentlyReading/${user.id}`, {
            method: "GET",
            headers: {
              "Access-Control-Allow-Origin": "https://localhost:7210",
              "Content-Type": "application/json",
            },
        })
        .then((res) => res.json())
        .then((data) => {
            syncBooks(data);
        });
    }, [userProfile]);

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