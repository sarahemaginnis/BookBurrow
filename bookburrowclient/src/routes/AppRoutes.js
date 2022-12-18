// index for router
import React, { useEffect, useState } from "react";
import { Route, Routes } from "react-router-dom";
import Authenticated from "../pages/Authenticated";
import BookPage from "../pages/Book/Book";
import PostPage from "../pages/Post/Post";
import RegisterUser from "../pages/Register/Register";
import UserProfile from "../pages/UserProfile/UserProfile";
import EditUserProfile from "../pages/EditUserProfile/EditUserProfile";

export default function AppRoutes({ user }) {
  const [currentUser, setCurrentUser] = useState({});

    //Verify user by firebaseUID
  //   useEffect(() => {
  //     console.log(currentUser);
  //     fetch(`https://localhost:7210/api/LoginViewModel/VerifyUser/${user.uid}`, {
  //         method: "GET",
  //         headers: {
  //           "Access-Control-Allow-Origin": "https://localhost:7210",
  //           "Content-Type": "application/json",
  //         },
  //     })
  //     .then((res) => (res.status === 200 ? res.json() : ""))
  //     .then((data) => {
  //         setCurrentUser(data);
  //     });
  // }, []);

  useEffect(() => {
    GetUser(user);
  }, []);

  const GetUser = () => {
    fetch(`https://localhost:7210/api/LoginViewModel/VerifyUser/${user.uid}`, {
      method: "GET",
      headers: {
        "Access-Control-Allow-Origin": "https://localhost:7210",
        "Content-Type": "application/json",
      },
    })
      .then((res) => (res.status === 200 ? res.json() : ""))
      .then((r) => {
        setCurrentUser(r);
      });
  };

  return currentUser === "" ? (
    <div>
      <Routes>
        <Route
          path="*"
          element={<RegisterUser user={user} setCurrentUser={setCurrentUser} />}
        />
      </Routes>
    </div>
  ) : (
    <div>
      <Routes>
        <Route exact path="/" element={<Authenticated user={user} currentUser={currentUser} />} />
        <Route path="/book/:bookId" element={<BookPage user={user} currentUser={currentUser} />} />
        <Route path="/user/:userProfileId" element={<UserProfile user={user} currentUser={currentUser} />} />
        <Route path="user/settings/:userId" element={<EditUserProfile user={user} currentUser={currentUser} />} />
        <Route path="post/:postId" element={<PostPage user={user} currentUser={currentUser} />} />
        <Route path="*" element={<Authenticated user={user} currentUser={currentUser} />} />
      </Routes>
    </div>
  );
}
