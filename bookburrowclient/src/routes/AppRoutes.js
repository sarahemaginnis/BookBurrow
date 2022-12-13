// index for router
import React, { useEffect, useState } from "react";
import { Route, Routes, useNavigate } from "react-router-dom";
import Authenticated from "../pages/Authenticated";
import RegisterUser from "../pages/Register";

export default function AppRoutes({ user }) {
  const [userExists, setUserExists] = useState({});
  const [currentUser, setCurrentUser] = useState({});

  const navigate = useNavigate();

  useEffect(() => {
    fetch(`https://localhost:7210/api/LoginViewModel/VerifyUser/${user.uid}`, {
      method: "GET",
      headers: {
        "Access-Control-Allow-Origin": "https://localhost:7210",
        "Content-Type": "application/json",
      },
    })
      .then((res) => res.json())
      .then((fbUser) => {
        fbUser === true ? setUserExists(fbUser) : RegisterUser();
      });
  }, []);

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
      .then((res) => res.json())
      .then((r) => {
        setCurrentUser(r);
      });
  };

  const RegisterUser = () => {
    console.log(user);
    const newUser = {
      name: user.displayName,
      email: user.email,
      firebaseId: user.uid,
      image: user.photoURL,
    }
    const fetchOptions = {
      method: 'POST',
      headers: {
        "Access-Control-Allow-Origin": "https://localhost:7210",
        "Content-Type": "application/json",
      },
      body: JSON.stringify(newUser)
    }
    return fetch('https://localhost:7210/api/User', fetchOptions)
    .then(res => res.json())
    .then(() => {
      navigate.push('/')
    })
  }

  console.log(userExists);
  console.log(currentUser);

  return (
    <div>
      <Routes>
        <Route exact path="/" element={<Authenticated user={user} currentUser={currentUser} />} />
        <Route path="/registration" element ={<RegisterUser user={user} />} />
        <Route path="*" element={<Authenticated user={user} currentUser={currentUser} />} />
      </Routes>
    </div>
  );
}
