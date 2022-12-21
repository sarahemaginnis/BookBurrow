import React, { useState, useEffect }  from "react";
import { Button, Col, Container, Row, Modal } from "react-bootstrap";
import { BsPencilFill, BsFillTrashFill } from "react-icons/bs";
import { useNavigate } from "react-router-dom";
import "./BookCard.css";

const BookCard = ({book, user, currentUser, userBook, userProfile}) => {
    const [bookStatuses, setBookStatuses] = useState([]); //State variable for array of book statuses
    const [currentUserBook, setCurrentUserBook] = useState(userBook) //can I do this, or does it need to be an empty object?
    const [userBookRatingId, setUserBookRatingId] = useState("");
    const [userBookRatingValue, setUserBookRatingValue] = useState("");
    // const [userBookStatus, setUserBookStatus] = useState({"name": "To be read", "value": 0});
    // const [userBookStatusName, setUserBookStatusName] = useState("To be read");
    // const [userBookStatusValue, setUserBookStatusValue] = useState(0);
    const [userBookReview, setUserBookReview] = useState("");
    
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);

    const navigation = useNavigate();

    const navigateToBookPage = () => {
        navigation(`/book/${book.bookAuthor.book.id}`)
    }

    //Add book to userBook (adding a book to the bookshelf)
    const AddBookToUserBookshelf = () => {
        const newUserBookObject = {
            bookId: book.bookAuthor.book.id,
            book: {
                id: book.bookAuthor.book.id,
                title: book.bookAuthor.book.title,
                isbn:book.bookAuthor.book.isbn,
                description: book.bookAuthor.book.description,
                coverImageUrl: book.bookAuthor.book.coverImageURl,
                datePublished: book.bookAuthor.book.datePublished,
                createdAt: book.bookAuthor.book.createdAt,
                updatedAt: book.bookAuthor.book.updatedAt,
            },
            userId: currentUser.id,
            userProfile: {
                id: userProfile.id,
                userId: userProfile.userId,
                profileImageUrl: userProfile.profileImageUrl,
                firstName: userProfile.firstName,
                lastName: userProfile.lastName,
                handle: userProfile.handle,
                pronounId: userProfile.pronounId,
                userPronoun: {
                    id: userProfile.userPronoun.id,
                    pronouns: userProfile.userPronoun.pronouns,
                },
                biography: userProfile.biography,
                biographyUrl: userProfile.biographyUrl,
                birthday: userProfile.birthday,
                createdAt: userProfile.createdAt,
                updatedAt: userProfile.updatedAt,
            },
            userPronoun: {
                id: userProfile.userPronoun.id,
                pronouns: userProfile.userPronoun.pronouns,
            },
            ratingId: userBookRatingId,
            rating: {
                id: userBookRatingId,
                displayValue: userBookRatingValue,
            },
            bookStatus: {
                name: userBookStatusName,
                value: userBookStatusValue,
            },
            review: userBookReview,
        }
        const fetchOptions = {
            method: 'POST',
            headers: {
                "Access-Control-Allow-Origin": "https://localhost:7210",
                "Content-Type": "application/json",
            },
            body: JSON.stringify(newUserBookObject)
        }
        return fetch('https://localhost:7210/api/UserBook', fetchOptions)
        .then(res => res.json())
        .then((res) => {
            console.log(res)
            setCurrentUserBook(res)
        })
        .then(() => {handleClose})
        .then(() => {navigateToBookPage()})
    }

    //Edit a userBook (put request)

    //need an event listener/event handler for updating Bookshelf

    //Delete book from bookshelves (delete UserBook)

    //need a conditional modal for bookshelf selection - once delete from shelf is clicked then the modal text needs to change
    //Are you sure you want to remove this book from your shelves?
    //Removing this book will clear associated ratings, reviews, and reading activity
    //Cancel & Remove

    //use find through array of book.bookStatusOptions to find the right button to display
    // const bookshelfButtonDisplay = () => {
    //     const foundShelf = () => {
    //         if (userBook.hasOwnProperty("userBook")){
    //             return book.bookStatusOptions.find(b => b.value === userBook.userBook.bookStatus.value)
    //         } else {
    //             return userBookStatus
    //         }
    //     }
    //     setUserBookStatus(foundShelf)
    //     if (currentUser.id === userBook?.userBook?.userId){
    //         return <Button className="btn-primary" onClick={(e) => {
    //             e.stopPropagation();
    //             setShow(true)
    //         }}><BsPencilFill />{userBookStatus.name}</Button>
    //     }
    // }

    //then use conditional logic to figure out which button should be displayed
    const bookshelfButton = () => {
        if (currentUser.id === userBook?.userBook?.userId && userBook.userBook.bookStatus.value === 1){
            return <Button className="btn-primary" onClick={(e) => {
                e.stopPropagation();
                setShow(true)
            }}><BsPencilFill />Currently reading</Button>
        } else if (currentUser.id === userBook?.userBook?.userId && userBook.userBook.bookStatus.value === 2) {
            return <Button className="btn-primary" onClick={(e) => {
                e.stopPropagation();
                setShow(true)
            }}><BsPencilFill />Read</Button>
        } else if (currentUser.id === userBook?.userBook?.userId && userBook.userBook.bookStatus.value === 3) {
            return <Button className="btn-primary" onClick={(e) => {
                e.stopPropagation();
                setShow(true)
            }}><BsPencilFill />Did not finish</Button>
        } else {
            return <Button className="btn-primary" onClick={(e) => {
                e.stopPropagation();
                setShow(true)
            }}>Want to read</Button>
        }
    }

    return (
        <div className="book-card">
            <Container>
            <Row>
                <Col sm={4}>
                    <img src={`${book.bookAuthor.book.coverImageUrl}`} />
                    {bookshelfButton()}
                    <p className="book-card-user-rating-star">Star rating component displaying currentUser's rating of the book (or no rating)</p>
                </Col>
                <Col sm={8}>
                    <h2 className="book-card-series">Series Title & Number (e.g., The Atlas #1)</h2>
                    <h1 className="book-card-title">{book.bookAuthor.book.title}</h1>
                    <h2 className="book-card-author">{book.bookAuthor.author.firstName} {book.bookAuthor.author.middleName} {book.bookAuthor.author.lastName}</h2>
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
                    <p className="book-card-description">{book.bookAuthor.book.description}</p>
                </Col>
            </Row>
            </Container>
            <Modal show={show} onHide={handleClose} size="lg" centered className="modal__delete">
                <Modal.Header className="modal__header" closeButton>Choose a shelf for this book</Modal.Header>
                <Modal.Body className="modal__body">
                    <Container>
                            {book.bookStatusOptions.map((b) => (
                            <Row>
                                <Col md={{offset: 4}}>
                                    <Button key={`bookStatus--${b.value}`} value={b.value}>{b.name}</Button>
                                </Col>
                            </Row>
                            ))}
                        <Row>
                            <Col md={{offset: 4}}>
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