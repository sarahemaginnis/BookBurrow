import React from "react";
import "./NewPostComponent.css";
import { Container, Col, Row } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import {GoTextSize} from "react-icons/go";
import {AiOutlineCamera} from "react-icons/ai";
import {ImQuotesLeft, ImLink} from "react-icons/im";
import {HiChatBubbleLeftRight} from "react-icons/hi2";
import {FaHeadphonesAlt} from "react-icons/fa";
import {BsCameraVideoFill} from "react-icons/bs";

export default function  NewPost({user, post}) {
    const navigate = useNavigate();

    const navigateToMakeText = () => {
        navigate('/new/text')
    }

    const navigateToMakePhoto = () => {
        navigate('/new/photo')
    }

    const navigateToMakeQuote = () => {
        navigate('/new/quote')
    }

    const navigateToMakeLink = () => {
        navigate('/new/link')
    }

    const navigateToMakeChat = () => {
        navigate('/new/chat')
    }

    const navigateToMakeAudio = () => {
        navigate('/new/audio')
    }

    const navigateToMakeVideo = () => {
        navigate('/new/audio')
    }
  
    return (
        <Container>
            <Row>
                <Col><GoTextSize onClick={navigateToMakeText} /></Col>
                <Col><AiOutlineCamera onClick={navigateToMakePhoto} /></Col>
                <Col><ImQuotesLeft onClick={navigateToMakeQuote} /></Col>
                <Col><ImLink onClick={navigateToMakeLink} /></Col>
                <Col><HiChatBubbleLeftRight onClick={navigateToMakeChat} /></Col>
                <Col><FaHeadphonesAlt onClick={navigateToMakeAudio} /></Col>
                <Col><BsCameraVideoFill onClick={navigateToMakeVideo}/></Col>
            </Row>
        </Container>
  );
};