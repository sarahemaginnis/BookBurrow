// index for router
import React, { useEffect, useState } from "react";
import { Route, Routes } from "react-router-dom";
import Authenticated from "../pages/Authenticated";
import RegisterUser from "../pages/Register/Register";

export default function AppRoutes({ user }) {
  const [currentUser, setCurrentUser] = useState({});

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

  console.log(currentUser);

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
        <Route
          exact
          path="/"
          element={<Authenticated user={user} currentUser={currentUser} />}
        />
        <Route
          path="*"
          element={<Authenticated user={user} currentUser={currentUser} />}
        />
      </Routes>
    </div>
  );
}
