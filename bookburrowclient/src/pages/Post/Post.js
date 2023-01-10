import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router";
import { Container, Row, Col, Modal, Button } from "react-bootstrap";
import {RiShareForwardLine} from "react-icons/ri";
import {FaRegComment, FaRegBookmark} from "react-icons/fa";
import {BiRepost} from "react-icons/bi";
import {BsSuitHeart, BsPencilFill, BsFillTrashFill} from "react-icons/bs";
import './Post.css';
import { styled } from '@mui/material/styles';
import Card from '@mui/material/Card';
import CardHeader from '@mui/material/CardHeader';
import CardMedia from '@mui/material/CardMedia';
import CardContent from '@mui/material/CardContent';
import CardActions from '@mui/material/CardActions';
import Collapse from '@mui/material/Collapse';
import Avatar from '@mui/material/Avatar';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import FavoriteIcon from '@mui/icons-material/Favorite';
import {FaShare, FaComment} from "react-icons/fa";
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import MoreVertIcon from '@mui/icons-material/MoreVert';

const ExpandMore = styled((props) => {
    const { expand, ...other } = props;
    return <IconButton {...other} />;
  })(({ theme, expand }) => ({
    transform: !expand ? 'rotate(0deg)' : 'rotate(180deg)',
    marginLeft: 'auto',
    transition: theme.transitions.create('transform', {
      duration: theme.transitions.duration.shortest,
    }),
  }));

export default function PostPage ({user, currentUser}) {
    const [post, setPost] = useState({}); //initial state variable for current post object
    const {postId} = useParams(); //variable storing the route parameter
    const [show, setShow] = useState(false);
    const [expanded, setExpanded] = React.useState(false);

    const handleClose = () => setShow(false);

    const handleExpandClick = () => {
        setExpanded(!expanded);
      };

    const navigate = useNavigate();

    const navigateHome = () => {
        navigate(`/`)
    }

    const navigateToEditPost = () => {
        navigate(`/post/edit/${postId}`)
      }

    const navigateToProfile = () => {
        navigate(`/user/${post.userProfile.userId}`)
    }

    //Get post detail information from API and update state when the value of postId changes
    useEffect(() => {
        console.log(postId);
        fetch(`https://localhost:7210/api/UserPost/${postId}`, {
            method: "GET",
            headers: {
              "Access-Control-Allow-Origin": "https://localhost:7210",
              "Content-Type": "application/json",
            },
        })
        .then((res) => res.json())
        .then((data) => {
            setPost(data);
        });
    }, [postId]);

    const editPostIconsShow = () => {
        return(<>
        <IconButton>
        <BsFillTrashFill style={{cursor: "pointer"}} onClick={(e) => {
            e.stopPropagation();
            setShow(true)
        }}/>
        </IconButton>
        <IconButton>
        <BsPencilFill style={{cursor: "pointer"}} onClick={navigateToEditPost} />
        </IconButton>
        </>)
    }

    //Delete userPost
    const DeletePost = (id) => {
        fetch(`https://localhost:7210/api/UserPost/${id}`, {
            method: "DELETE"
        })
        .then(handleClose)
        .then(() => {
            navigateHome()
        });
    }

//pass down post object and user and currentUser object into components to render properly and pass into fetch calls    
  return ( post.hasOwnProperty("id") ? 
    <>
    <Container>
    <Row>
    <Col>
    <Card>
        <CardHeader />
            <Container>
                <Row>
                    <Col xs={1}>
                    <Avatar 
                        alt={post.userProfile.handle}
                        src={post.userProfile.profileImageUrl}
                        sx={{width: 56, height: 56}}
                        onClick={navigateToProfile} 
                        style={{cursor: "pointer"}} 
                    />
                    </Col>
                    <Col><p onClick={navigateToProfile} style={{cursor: "pointer"}} ><b>{post.userProfile.handle}</b></p></Col>
                </Row>
            </Container>
        <CardMedia 
            component="img"
            image={post.cloudinaryUrl}
            alt={post.title}
        />
        <CardContent>
            <Typography paragraph>
                {post.title}
            </Typography>
            <Typography paragraph variant="body2" color="text.secondary">
                {post.caption}
            </Typography>
            <Typography paragraph variant="body2" color="text.secondary">
                {post.source}
            </Typography>
        </CardContent>
        <CardActions disableSpacing>
                    <ExpandMore
                    expand={expanded}
                    onClick={handleExpandClick}
                    aria-expanded={expanded}
                    aria-label="show more"
                    >
                    <ExpandMoreIcon />
                    </ExpandMore>
                    <IconButton aria-label="share">
                    <FaShare />
                    </IconButton>
                    <IconButton aria-label="comment">
                        <FaComment />
                    </IconButton>
                    <IconButton aria-label="reblog">
                        <BiRepost />
                    </IconButton>
                    <IconButton aria-label="add to favorites">
                    <FavoriteIcon />
                    </IconButton>
                    {currentUser.id === post.userProfile.userId ? editPostIconsShow() : <Col></Col>}
                </CardActions>
                <Collapse in={expanded} timeout="auto" unmountOnExit>
                    <CardContent>
                    <Typography paragraph>Comments would go here, in the space below:</Typography>
                    </CardContent>
                </Collapse>
    </Card>
    </Col>
    </Row>
    </Container>
    <Modal show={show} onHide={handleClose} size="lg" centered className="modal__delete">
        <Modal.Header className="modal__header" closeButton></Modal.Header>
        <Modal.Body className="modal__body">
            <h4>Are you sure you want to delete this post?</h4>
            <p>This action cannot be undone.</p>
        </Modal.Body>
        <Modal.Footer className="modal__footer">
            <Button className="btn__btn-secondary" onClick={(e) => {
                e.stopPropagation();
                setShow(false)
            }}>Cancel</Button>
            <Button className="btn__btn-primary" onClick={() => DeletePost(postId)}>Delete</Button>
        </Modal.Footer>
    </Modal>
    </> : null
  );
};