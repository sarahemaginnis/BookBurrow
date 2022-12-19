import React, { useState, useEffect }  from "react";
import { Button, Col, Container, Row, Modal } from "react-bootstrap";
import { BsPencilFill, BsFillTrashFill } from "react-icons/bs";
import "./BookCard.css";

const BookCard = ({book, user, currentUser}) => {
    const [userBookObject, setUserBookObject] = useState({}); //initial state variable for current userBook object
    const [bookStatuses, setBookStatuses] = useState([]); //State variable for array of book statuses
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    //get array of Book statuses - currently hardcoded in

    //Get userBook information from API and update state when the value of currenUser.id changes
    useEffect(() => {
        fetch(`https://localhost:7210/api/UserBook/${currentUser.id}`, {
            method: "GET",
            headers: {
              "Access-Control-Allow-Origin": "https://localhost:7210",
              "Content-Type": "application/json",
            },
        })
        .then((res) => res.json())
        .then((data) => {
            setUserBookObject(data)
        })
    }, []);

    //need an event listener/event handler for updating Bookshelf

    //Delete book from bookshelves (delete UserBook)

    //need a conditional modal for bookshelf selection - once delete from shelf is clicked then the modal text needs to change
    //Are you sure you want to remove this book from your shelves?
    //Removing this book will clear associated ratings, reviews, and reading activity
    //Cancel & Remove

    const bookshelfButton = () => {
        if (currentUser.id === userBookObject.userId && userBookObject.bookStatus.value === 2){
            return <Button className="btn-primary" onClick={(e) => {
                e.stopPropagation();
                setShow(true)
            }}><BsPencilFill />Currently Reading</Button>
        } else if (currentUser.id === userBookObject.userId && userBookObject.bookStatus.value === 3) {
            return <Button className="btn-primary" onClick={(e) => {
                e.stopPropagation();
                setShow(true)
            }}><BsPencilFill />Read</Button>
        } else if (currentUser.id === userBookObject.userId && userBookObject.bookStatus.value === 4) {
            return <Button className="btn-primary" onClick={(e) => {
                e.stopPropagation();
                setShow(true)
            }}><BsPencilFill />Did not finish</Button>
        } else {
            return <Button className="btn-primary" onClick={(e) => {
                e.stopPropagation();
                setShow(true)
            }}>To be read</Button>
        }
    }

    return ( 
        <div className="book-card">
            <Container>
            <Row>
                <Col sm={4}>
                    <img src={`${book.book.coverImageUrl}`} />
                    {bookshelfButton()}
                    <p className="book-card-user-rating-star">Star rating component displaying currentUser's rating of the book (or no rating)</p>
                </Col>
                <Col sm={8}>
                    <h2 className="book-card-series">Series Title & Number (e.g., The Atlas #1)</h2>
                    <h1 className="book-card-title">{book.book.title}</h1>
                    <h2 className="book-card-author">{book.author.firstName} {book.author.middleName} {book.author.lastName}</h2>
                    <Row>
                        <Col>
                            <p className="book-card-average-rating-star">Star rating component displaying average rating amongst all users</p>
                        </Col>
                        <Col>
                            <p className="book-card-average-rating-numerical">Average numerical rating amonst all users</p>
                        </Col>
                        <Col>
                            <p className="book-card-total-ratings">Total number of ratings</p>
                        </Col>
                        <Col>
                            <p className="book-card-total-reviews">Total number of reviews</p>
                        </Col>
                    </Row>
                    <p className="book-card-description">{book.book.description}</p>
                </Col>
            </Row>
            </Container>
            <Modal show={show} onHide={handleClose} size="lg" centered className="modal__delete">
                <Modal.Header className="modal__header" closeButton>Choose a shelf for this book</Modal.Header>
                <Modal.Body className="modal__body">
                    <Container>
                        <Row>
                            <Col>
                                <Button>Want to read</Button>
                            </Col>
                        </Row>
                        <Row>
                            <Col>
                                <Button>Currently reading</Button>
                            </Col>
                        </Row>
                        <Row>
                            <Col>
                                <Button>Read</Button>
                            </Col>
                        </Row>
                        <Row>
                            <Col>
                                <Button>Did not finish</Button>
                            </Col>
                        </Row>
                        <Row>
                            <Col>
                                <Button><BsFillTrashFill />Remove from my shelf</Button>
                            </Col>
                        </Row>
                    </Container>
                </Modal.Body>
                <Modal.Footer className="modal__footer">
                </Modal.Footer>
        </Modal>
        </div>
    );
};

export default BookCard;