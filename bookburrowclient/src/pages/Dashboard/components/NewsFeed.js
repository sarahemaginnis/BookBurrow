import * as React from 'react';
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
import {BiRepost} from "react-icons/bi";
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import MoreVertIcon from '@mui/icons-material/MoreVert';
import { Container, Row, Col } from 'react-bootstrap';
import { useNavigate } from 'react-router';
import "./NewsFeed.css";

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

export default function NewsFeedCard(posts) {
    console.log(posts.posts);
  const [expanded, setExpanded] = React.useState(false);

  const navigate = useNavigate();

  const handleExpandClick = () => {
    setExpanded(!expanded);
  };

  return (
    <div>
        <Container>
            <Row>
        {Array.isArray(posts.posts) ? (posts.posts.map((post) => {
            const navigateToPost = () => {
                navigate(`post/${post.id}`)
            }
            const navigateToProfile = () => {
                navigate(`user/${post.userProfile.userId}`)
            }
            return (
                <Row>
                    <Col xs={1}>
                        <Avatar 
                            alt={post.userProfile ?post.userProfile.handle : null} 
                            src={post.userProfile ? post.userProfile.profileImageUrl : null}
                            sx={{width: 56, height: 56}}
                            onClick={navigateToProfile}
                            style={{cursor: "pointer"}}
                        />
                    </Col>
                <Col>
                <Card>
                <CardHeader
                    action={
                        <IconButton aria-label="settings">
                        <MoreVertIcon />
                    </IconButton>
                    }
                    title={post.userProfile ? post.userProfile.handle : null}
                    />
                <CardMedia
                    component="img"
                    image={post.cloudinaryUrl ? post.cloudinaryUrl : post.book.coverImageUrl}
                    alt={post.title ? post.title : null}
                    onClick={navigateToPost} style={{cursor: "pointer"}}
                    />
                <CardContent>
                    <Typography paragraph>
                        {post.title ? post.title : null}
                    </Typography>
                    <Typography paragraph variant="body2" color="text.secondary">
                    {post.caption ? post.caption : null}
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
                </CardActions>
                <Collapse in={expanded} timeout="auto" unmountOnExit>
                    <CardContent>
                    <Typography paragraph>Comments would go here, in the space below:</Typography>
                    </CardContent>
                </Collapse>
                </Card>
                </Col>
                </Row>
            )
        })
        ) : (<div></div>)
    }
    </Row>
    </Container>
    </div>
  );
}