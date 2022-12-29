import React, { useState, useEffect }  from "react";
import { Button, Col, Container, Row, Modal } from "react-bootstrap";
import { BsPencilFill, BsFillTrashFill } from "react-icons/bs";
import { useNavigate } from "react-router-dom";
import "./BookCard.css";

const BookCard = ({book, user, userBook, currentUser, userProfile, bookStatusOptions}) => {
    const [currentUserBook, setCurrentUserBook] = useState({});
    const [currentUserBookTest, setCurrentUserBookTest] = useState({});
    const [userBookStatus, setUserBookStatus] = useState({});
    const [userBookRatingId, setUserBookRatingId] = useState("");
    const [userBookRatingValue, setUserBookRatingValue] = useState("");
    const [userBookReview, setUserBookReview] = useState("");
    const [modalView, setModalView] = useState(1);
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);

    const navigation = useNavigate();

    const navigateToBookPage = () => {
        navigation(`/book/${book.bookAuthor.book.id}`)
    }

    const submitEdits = () => {
        navigation(`/book/${currentUserBookTest.bookId}`)
    }

    const bookStatus0 = book.bookStatusOptions.find(({value}) => value === 0);
    const bookStatus1 = book.bookStatusOptions.find(({value}) => value === 1);
    const bookStatus2 = book.bookStatusOptions.find(({value}) => value === 2);
    const bookStatus3 = book.bookStatusOptions.find(({value}) => value === 3);

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
                name: userBookStatus.name,
                value: userBookStatus.value,
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
    const UpdateUserBook = (evt) => {
        evt.preventDefault();
        //Construct a new object to replace the exisitng one in the API
        const updatedUserBook = {
            id:currentUserBook.id,
            bookId: currentUserBook.bookId,
            book: {
                id: currentUserBook.book.id,
                title: currentUserBook.book.title,
                isbn:currentUserBook.book.isbn,
                description: currentUserBook.book.description,
                coverImageUrl: currentUserBook.book.coverImageURl,
                datePublished: currentUserBook.book.datePublished,
                createdAt: currentUserBook.book.createdAt,
                updatedAt: currentUserBook.book.updatedAt,
            },
            userId: currentUserBook.userId,
            userProfile: {
                id: currentUserBook.userProfile.id,
                userId: currentUserBook.userProfile.userId,
                profileImageUrl: currentUserBook.userProfile.profileImageUrl,
                firstName: currentUserBook.userProfile.firstName,
                lastName: currentUserBook.userProfile.lastName,
                handle: currentUserBook.userProfile.handle,
                pronounId: currentUserBook.userProfile.pronounId,
                userPronoun: {
                    id: currentUserBook.userProfile.userPronoun.id,
                    pronouns: currentUserBook.userProfile.userPronoun.pronouns,
                },
                biography: currentUserBook.userProfile.biography,
                biographyUrl: currentUserBook.userProfile.biographyUrl,
                birthday: currentUserBook.userProfile.birthday,
                createdAt: currentUserBook.userProfile.createdAt,
                updatedAt: currentUserBook.userProfile.updatedAt,
            },
            userPronoun: {
                id: currentUserBook.userPronoun.id,
                pronouns: currentUserBook.userPronoun.pronouns,
            },
            ratingId: currentUserBook.ratingId,
            rating: {
                id: currentUserBook.rating.id,
                displayValue: currentUserBook.rating.displayValue,
            },
            bookStatus: {
                name: currentUserBook.bookStatus.name,
                value: currentUserBook.bookStatus.value,
            },
            review: currentUserBook.review,
            reviewCreatedAt: currentUserBook.reviewCreatedAt,
            reviewUpdatedAt: Date.now,
        };
        //Perform the PUT request to replace the object
        fetch(`https://localhost:7210/api/UserBook/${currentUserBook.id}`, {
            method: "PUT",
            headers: {
                "Access-Control-Allow-Origin": "https://localhost:7210",
                "Content-Type": "application/json",
            },
            body: JSON.stringify(updatedUserBook),
        }).then(() => {
            submitEdits()
        });
    }

    const tryChangeStatus = (requestObject) => {
        return fetch(`https://localhost:7210/api/UserBook/TryChangeStatus`, {
            method: "PUT",
            headers: {
                "Access-Control-Allow-Origin": "https://localhost:7210",
                "Content-Type": "application/json",
            },
            body: JSON.stringify(requestObject),
        }).then((res) => res.json())
    }

    //handleClick function for updating userBookStatus
    //do a fetch call, in the body would be the user.id, book.id, and status.value
    //endpoint should probably be a PUT UserBook/TryChangeStatus
    const handleBookShelfClick = async (userBookStatus) => {
        setUserBookStatus(userBookStatus)
        const requestObject = {userId: currentUser.id, bookId: book.bookAuthor.book.id, bookStatus: userBookStatus}
        //do the fetch call here
        const data = await tryChangeStatus(requestObject)
            console.log({data})
            setCurrentUserBookTest(data)
            console.log(data)
            const ub = currentUserBookTest
            console.log(ub)
            console.log({currentUserBookTest})
            handleClose()
            submitEdits()
    }

    //conditional logic to determine if userBook exists for POST or PUT
    const checkUserBookExists = () => {
        if(Object.keys(currentUserBook).length === 0){
            AddBookToUserBookshelf()
        } else {
            UpdateUserBook()
        }
    }

    //Delete book from bookshelves (delete UserBook)
    const DeleteBookFromShelf = (id) => {
        fetch(`https://localhost:7210/api/UserBook/${id}`, {
            method: "DELETE"
        })
        .then(handleClose)
        .then(setModalView(1))
        .then(() => {
            navigateToBookPage()
        })
    }

    //then use conditional logic to figure out which button should be displayed
    const bookshelfButton = () => {
        if (currentUser.id === userBook?.userBook?.userId && userBook.userBook.bookStatus.value === 1){
            return <Button className="btn-primary" onClick={(e) => {
                e.stopPropagation();
                setShow(true)
            }}><BsPencilFill />{bookStatus1.name}</Button>
        } else if (currentUser.id === userBook?.userBook?.userId && userBook.userBook.bookStatus.value === 2) {
            return <Button className="btn-primary" onClick={(e) => {
                e.stopPropagation();
                setShow(true)
            }}><BsPencilFill />{bookStatus2.name}</Button>
        } else if (currentUser.id === userBook?.userBook?.userId && userBook.userBook.bookStatus.value === 3) {
            return <Button className="btn-primary" onClick={(e) => {
                e.stopPropagation();
                setShow(true)
            }}><BsPencilFill />{bookStatus3.name}</Button>
        } else {
            return <Button className="btn-primary" onClick={(e) => {
                e.stopPropagation();
                setShow(true)
            }}>{bookStatus0.name}</Button>
        }
    }

    const modalViewSwitch = () => {
        if(modalView === 1 && userBook.hasOwnProperty("id")) {
            return <div>
            <Modal show={show} onHide={handleClose} size="lg" centered className="modal__delete">
                <Modal.Header className="modal__header" closeButton>Choose a shelf for this book</Modal.Header>
            <Modal.Body className="modal__body">
                <Container>
                    {book.bookStatusOptions.map((b) => (
                    <Row>
                        <Col md={{offset: 4}}>
                            <Button type="submit" key={`bookStatus--${b.value}`} value={b.value} onClick={(e) => {
                                e.stopPropagation();
                                handleBookShelfClick(b);
                            }}>{b.name}</Button>
                        </Col>
                    </Row>
                    ))}
                    <Row>
                        <Col md={{offset: 4}}>
                            <Button onClick={(e) => {
                                e.stopPropagation();
                                setModalView(2);
                            }} ><BsFillTrashFill/>Remove from my shelf</Button>
                        </Col>
                    </Row>
                </Container>
            </Modal.Body>
        <Modal.Footer className="modal__footer">
        </Modal.Footer>
        </Modal>
        </div>
        } else if(modalView === 1 ) {
            return <div>
            <Modal show={show} onHide={handleClose} size="lg" centered className="modal__delete">
                <Modal.Header className="modal__header" closeButton>Choose a shelf for this book</Modal.Header>
            <Modal.Body className="modal__body">
                <Container>
                    {book.bookStatusOptions.map((b) => (
                    <Row>
                        <Col md={{offset: 4}}>
                            <Button type="submit" key={`bookStatus--${b.value}`} value={b.value} onClick={() => handleBookShelfClick(b)}>{b.name}</Button>
                        </Col>
                    </Row>
                    ))}
                </Container>
            </Modal.Body>
        <Modal.Footer className="modal__footer">
        </Modal.Footer>
        </Modal>
        </div>
        }
        else if(modalView === 2){
            return <div>
                <Modal show={show} onHide={handleClose} size="lg" centered className="modal__delete">
            <Modal.Header className="modal__header" closeButton>Are you sure you want to remove this book from your shelves?</Modal.Header>
            <Modal.Body className="modal__body">
                <Container>
                    <Row>
                        <Col>
                        <p>Removing this book will clear associated ratings, reviews, and reading activity.</p>
                        </Col>
                    </Row>
                </Container>
            </Modal.Body>
            <Modal.Footer className="modal__footer">
                <Button className="btn__btn-secondary" onClick={() => {
                    setShow(false); 
                    setModalView(1)
                    }}>Cancel</Button>
                <Button className="btn__btn-primary" onClick={() => DeleteBookFromShelf(currentUserBook.id)}>Delete</Button>
            </Modal.Footer>
            </Modal>
        </div>
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
            {modalViewSwitch()}
        </div>
    );
};

export default BookCard;