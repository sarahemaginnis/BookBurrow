import React, {useState} from "react";
import { Button, Modal } from "react-bootstrap";
import logo from "../BookBurrowLogo.png";

export default function RegisterModal ({ user, setCurrentUser }) {
    const [show, setShow] = useState(false);
    const[modalState, setModalState] = useState<"modal-one" | "modal-two" | "modal-three" | "close">("close")

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const handleShowModalOne = () => {setModalState("modal-one")}
    const handleShowModalTwo = () => {setModalState("modal-two")}
    const handleShowModalThree = () => {setModalState("modal-three")}

    console.log(show);

    return(
        <>
            <Modal show={modalState === "modal-one"} fullscreen="fullscreen" onHide={handleShowModalTwo} backdrop="static" keyboard={false}>
                <Modal.Header closeButton>
                    <Modal.Title>This is Modal One</Modal.Title>
                    <img src={logo} />
                </Modal.Header>
                <Modal.Body>
                    <p>Before we go further:</p>
                    <p>When is your birthday?</p>
                    <p>We'll never share this with other users.</p>
                    <p>We're just making sure you're old enough to use Book Burrow</p>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleShowModalTwo}>Next</Button>
                </Modal.Footer>
            </Modal>

            <Modal show={modalState === "modal-two"} fullscreen="fullscreen" onHide={handleShowModalThree} backdrop="static" keyboard={false}>
                <Modal.Header closeButton>
                    <Modal.Title>This is Modal Two</Modal.Title>
                    <img src={logo} />
                </Modal.Header>
                <Modal.Body>
                    <p>What should we call you?</p>
                    <p>This will  be how you appear to others on Book Burrow, and your URL. Don't worry, you can change this later.</p>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleShowModalThree}>Next</Button>
                </Modal.Footer>
            </Modal>

            <Modal show={modalState === "modal-three"} fullscreen="fullscreen" onHide={handleClose} backdrop="static" keyboard={false}>
                <Modal.Header closeButton>
                    <img src={logo} />
                    <Modal.Title>Welcome, {user.displayname}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <p>Book Burrow exists because reading is better together.</p>
                    <p>The content on Book Burrow is created for and by readers. It represents the many different perspectives of the community.</p>
                    <p>RULES OF CONDUCT</p>
                    <p>No discrimination of any kind. No transphobia, no racism, no ableism, no religious discrimination. No hate speech or bullying of any kind. We reserve the right to delete posts for any reason.</p>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>I Agree</Button>
                </Modal.Footer>
            </Modal>
        </>
    );
};