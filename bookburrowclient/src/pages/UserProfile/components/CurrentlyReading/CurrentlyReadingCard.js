import React from "react";
import { Container, Button, Col, Row } from "react-bootstrap";
import { useNavigate } from "react-router";
import "./CurrentlyReading.css";

const CurrentlyReadingCard = ({userProfile, user, currentUser, userBooks, bookAuthors}) => {
    const navigate = useNavigate();

    return (
        <Container>
            <Row xs={1} md={2}>
            {userBooks.length > 0 ? (userBooks.filter(userBook => userBook.bookStatus.name === "Currently reading").map((userBook) => {
                const navigateToBook = () => {
                    navigate(`/book/${userBook.bookId}`)
                }
                const foundBook = bookAuthors.find(({bookId}) => bookId === userBook.bookId)
                console.log(foundBook);
                return (
                    <Row>
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
                        </Col>
                        <Col>
                            <Row>
                                <Button>Update progress</Button>
                            </Row>
                        </Col>
                    </Row>
                )
            })
            ) : (<div></div>)}
            </Row>
        </Container>
    );
};

export default CurrentlyReadingCard;