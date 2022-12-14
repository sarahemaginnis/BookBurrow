import React, { useEffect, useState } from "react";
import { useParams } from "react-router";
import { Container, Row } from "react-bootstrap";
import AuthorCard from "./components/AuthorCard";
import BookCard from "./components/BookCard";
import BurrowCard from "./components/BurrowCard";
import ReviewCard from "./components/ReviewCard";
import './Book.css';

export default function BookPage ({user, currentUser}) {
    const [book, setBook] = useState({}); //initial state variable for current book object
    const {bookId} = useParams(); //variable storing the route parameter

    //Get book detail information from API and update state when the value of bookId changes
    useEffect(() => {
        console.log(bookId);
        fetch(`https://localhost:7210/api/BookViewModel/GetBook/${bookId}`, {
            method: "GET",
            headers: {
              "Access-Control-Allow-Origin": "https://localhost:7210",
              "Content-Type": "application/json",
            },
        })
        .then((res) => res.json())
        .then((data) => {
            setBook(data);
        });
    }, [bookId]);

//pass down book object and user and currentUser object into components to render properly and pass into fetch calls    
  return ( book.hasOwnProperty("id") ? 
    <>
      <Container>
        <BookCard book={book} />
        <AuthorCard book={book} />
        <ReviewCard book={book} />
        <BurrowCard book={book} />
      </Container>
    </> : null
  );
};