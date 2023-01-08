import React from "react";
import "./CurrentlyReading.css";
import {Container, Button, Col, Row } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

export default function CurrentlyReadingGrid({userBooks, bookAuthors}) {
    const navigate = useNavigate();

    return (
        <Container>
            {userBooks.length > 0 ? (userBooks.map((userBook) => {
                const navigateToBook = () => {
                    navigate(`/book/${userBook.bookId}`)
                }
                const foundBook = bookAuthors.find(({bookId}) => bookId === userBook.bookId)
                console.log(foundBook);
                return (
                    <Row className="mb-2">
                        <Col>
                            <img src={userBook.book ? userBook.book.coverImageUrl : null} onClick={navigateToBook}/>
                        </Col>
                        <Col>
                            <Row>
                                <p onClick={navigateToBook}>{userBook.book ? userBook.book.title : null}</p>
                            </Row>
                            <Row>
                                <p>by {userBook.book ? foundBook.author.firstName : null} {userBook.book ? foundBook.author.middleName : null} {userBook.book ? foundBook.author.lastName : null}</p>
                            </Row>
                            <Row>
                                <Button>Update progress</Button>
                            </Row>
                        </Col>
                    </Row>
                )
            })
            ) : (<div></div>)}
            </Container>
    );
};