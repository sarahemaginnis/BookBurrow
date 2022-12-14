import React from "react";
import { Button, Col, Row } from "react-bootstrap";
import { BsPencilFill } from "react-icons/bs";
import "./BookCard.css";

const BookCard = ({book, user, currentUser}) => {

    //need a modal for bookshelf selection

    return (
        <div className="book-card">
            <Row>
                <Col sm={4}>
                    <img src={`${book.book.coverImageUrl}`} />
                    <Button><BsPencilFill />Book Shelf Button</Button>
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
        </div>
    );
};

export default BookCard;