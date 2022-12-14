import React, { useState, useEffect } from "react";
import { Form, Button, InputGroup } from "react-bootstrap";
import logo from "./BookBurrowLogo.png";
import RegisterModal from "./components/RegisterModal";

export default function RegisterUser({ user, setCurrentUser }) {
  const [date, setDate] = useState(new Date());
  const [userProfile, setUserProfile] = useState({}); //initial state variable for user profile object

  const [userProfileImage, setUserProfileImage] = useState("");
  const [userFirstName, setUserFirstName] = useState("");
  const [userLastName, setUserLastName] = useState("");
  const [userHandle, setUserHandle] = useState("");
  const [userPronounId, setUserPronounId] = useState("");
  const [userBiography, setUserBiography] = useState("");
  const [userBiographyUrl, setUserBiographyUrl] = useState("");
  const [userBirthday, setUserBirthday] = useState("");
    
      const RegisterUser = () => {
        const newUser = {
          email: user.email,
          firebaseUID: user.uid
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
        .then((res) => {
            setCurrentUser(res)
        })
      }

      const RegisterProfile = () => {
        const newUserProfile = {
          userId: user.id,
          profileImageUrl: //update these fields,
          firstName: ,
          lastName: ,
          handle: ,
          pronounId: ,
          biography: ,
          biographyUrl: ,
          birthday:
        }
        const fetchOptions = {
          method: 'POST',
          headers: {
            "Access-Control-Allow-Origin": "https://localhost:7210",
            "Content-Type": "application/json",
          },
          body: JSON.stringify(newUserProfile)
        }
        return fetch('https://localhost:7210/api/UserProfile', fetchOptions) //update with correct url path for posting new userprofile
        .then(res => res.json())
        .then((res) => {
          setUserProfile(res)
        })
      }

  return (
    user ?
    <>
    <RegisterModal />
      <img
        className="image__login"
        src={logo}
        alt="Book Burrow Logo"
      />
      <Form>
        <Form.Group className="mb-3" controlId="formBasicPhotoUrl">
          <Form.Label>Profile Image</Form.Label>
          <Form.Control type="text" placeholder="Photo url" defaultValue={user.photoURL} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicFirstName">
          <Form.Label>First Name</Form.Label>
          <Form.Control type="text" placeholder="First name" defaultValue={user.displayName} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicLastName">
          <Form.Label>Last Name</Form.Label>
          <Form.Control type="text" placeholder="Last name" defaultValue={user.displayName} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicHandle">
          <Form.Label>Handle</Form.Label>
          <InputGroup>
          <InputGroup.Text id="inputGroupPrepend">@</InputGroup.Text>
          </InputGroup>
          <Form.Control type="text" placeholder="handle" />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicPronouns">
          <Form.Select aria-label="Default select example">
            <option>Preferred Pronouns</option>
            <option value="1">he/him/his</option>
            <option value="2">she/her/hers</option>
            <option vlue="3">they/them</option>
          </Form.Select>
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicBiography">
          <Form.Label>Biography</Form.Label>
          <Form.Control as="textarea" rows={3} />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicBiographyUrl">
          <Form.Label>Featured Link (url)</Form.Label>
          <Form.Control type="text" placeholder="e.g., www.yourawesomewebsite.com" />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicBirthday">
        <Form.Label>Birthday</Form.Label>
          <Form.Control type="date" placeholder="Date of birth" value={date} onChange={(e) => setDate(e.target.value)} />
        </Form.Group>
      </Form>
      <button type="submit" onClick={() => {RegisterUser(); RegisterProfile();}}>Register</button>
    </> : null
  );
}
