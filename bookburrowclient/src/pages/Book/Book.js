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
    const [bookStatuses, setBookStatuses] = useState([]); //State variable for array of book statuses
    const [userBookObject, setUserBookObject] = useState({}); //initial state variable for current userBook object
    const [userProfileObject, setUserProfileObject] = useState({}); //initial state variable for currentUser userProfile object

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
        })
        .then(() => {
          setBookStatuses(book.bookStatusOptions)
        });
      }, []);

    //Get userBook information from API and update state when the value of currenUser.id changes
    const GetUserBook = () => {
      console.log(currentUser.id)
      fetch (`https://localhost:7210/api/UserBook/${currentUser.id}`, {
        method: "GET",
        headers: {
          "Access-Control-Allow-Origin": "https://localhost:7210",
          "Content-Type": "application/json",
      },
    })
    .then((res) => res.json())
    .then((data) => {
      setUserBookObject(data)
      console.log("setting user book", userBookObject)
    })
  }
  
  //Get userProfile
  const GetUserProfile = () => {
    console.log(currentUser.id)
    fetch (`https://localhost:7210/api/UserProfile/UserProfileByUserId/${currentUser.id}`, {
        method: "GET",
        headers: {
          "Access-Control-Allow-Origin": "https://localhost:7210",
          "Content-Type": "application/json",
      },
    })
    .then((res) => res.json())
    .then((data) => {
      setUserProfileObject(data)
      console.log("setting user profile", userProfileObject)
    })
  }

  useEffect(() => {
    if(book.hasOwnProperty("bookAuthor")){GetUserBook() ; GetUserProfile() ; console.log("getting user book", book, bookStatuses)}
  }, [book]);

//pass down book object and user and currentUser object into components to render properly and pass into fetch calls    
  return ( userProfileObject.hasOwnProperty("id") ? 
    <>
      <Container>
        <BookCard 
          book={book} 
          user={user} 
          currentUser={currentUser} 
          userBook={userBookObject} 
          userProfile={userProfileObject} 
          bookStatusOptions={bookStatuses} />
        <AuthorCard book={book} user={user} currentUser={currentUser} userBook={userBookObject} userProfile={userProfileObject} bookStatusOptions={bookStatuses}/>
        <ReviewCard book={book} user={user} currentUser={currentUser} userBook={userBookObject} userProfile={userProfileObject} bookStatusOptions={bookStatuses}/>
        <BurrowCard book={book} user={user} currentUser={currentUser} userBook={userBookObject} userProfile={userProfileObject} bookStatusOptions={bookStatuses}/>
      </Container>
    </> : null
  );
};