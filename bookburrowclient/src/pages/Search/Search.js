import SearchBox from "./components/SearchBox";
import "./Search.css";
import React, { useState } from "react";
import { Container, Card, Row, Col, Tabs, Tab } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

export default function Search({
  user,
  currentUser,
  books,
  searchBookValue,
  setSearchBookValue,
  userProfiles,
  searchUserProfileValue,
  setSearchUserProfileValue,
  userPosts,
  searchUserPostValue,
  setSearchUserPostValue
}) {

  const [key, setKey] = useState('books');
  const navigate = useNavigate();

  const setSearchBox = () => {
    if(key==='books') {
        return <SearchBox
          searchValue={searchBookValue}
          setSearchValue={setSearchBookValue}
        />
    } else if(key==='people') {
        return <SearchBox
          searchValue={searchUserProfileValue}
          setSearchValue={setSearchUserProfileValue}
        />
    } else if(key==='posts') {
        return <SearchBox
          searchValue={searchUserPostValue}
          setSearchValue={setSearchUserPostValue}
        />
    }
  }

  return (
    <div>
      <Container>
        <h1>Search</h1>
        {setSearchBox()}
        {/* <SearchBox
          searchValue={searchBookValue}
          setSearchValue={setSearchBookValue}
        /> */}
        <Container>
          <Tabs
            activeKey={key}
            onSelect={(k) => setKey(k)}
          >
            <Tab eventKey="books" title="Books">
              <div>
                <Container>
                  <Row xs={1} md={3} className="g-3">
                    {books.length > 0 ? (
                      books.map((book) => {
                        const navigateToBook = () => {
                          navigate(`/book/${book.bookId}`);
                        };
                        return (
                          <Col>
                            <Card
                              className="burrow-book-card"
                              onClick={navigateToBook}
                              style={{ cursor: "pointer" }}
                            >
                              <Card.Img
                                variant="top"
                                src={
                                  book.bookId ? book.book.coverImageUrl : null
                                }
                              />
                              <Card.Body>
                                <Card.Title>
                                  {book.bookId ? book.book.title : null}
                                </Card.Title>
                                <Card.Subtitle>
                                  {book.bookId ? book.author.firstName : null}{" "}
                                  {book.bookId ? book.author.middleName : null}{" "}
                                  {book.bookId ? book.author.lastName : null}
                                </Card.Subtitle>
                                <Card.Text>
                                  {book.bookId ? book.book.description : null}
                                </Card.Text>
                              </Card.Body>
                            </Card>
                          </Col>
                        );
                      })
                    ) : (
                      <div></div>
                    )}
                  </Row>
                </Container>
              </div>
            </Tab>
            <Tab eventKey="posts" title="Posts">
            <div>
                <Container>
                  <Row xs={1} md={3} className="g-3">
                    {userPosts.length > 0 ? (
                      userPosts.map((post) => {
                        const navigateToPost = () => {
                          navigate(`/post/${post.id}`);
                        };
                        return (
                          <Col>
                            <Card
                              className="burrow-book-card"
                              onClick={navigateToPost}
                              style={{ cursor: "pointer" }}
                            >
                              <Card.Img
                                variant="top"
                                src={
                                  post.cloudinaryUrl ? post.cloudinaryUrl : post.book.coverImageUrl
                                }
                              />
                              <Card.Body>
                                <Card.Title>
                                  {post.id ? post.title : null}
                                </Card.Title>
                                <Card.Subtitle>
                                  {post.id ? post.userProfile.handle : null}
                                </Card.Subtitle>
                                <Card.Text>
                                  {post.id ? post.caption : null}
                                </Card.Text>
                              </Card.Body>
                            </Card>
                          </Col>
                        );
                      })
                    ) : (
                      <div></div>
                    )}
                  </Row>
                </Container>
              </div>
            </Tab>
            <Tab eventKey="people" title="People">
            <div>
                <Container>
                  <Row xs={1} md={3} className="g-3">
                    {userProfiles.length > 0 ? (
                      userProfiles.map((userProfile) => {
                        const navigateToProfile = () => {
                          navigate(`/user/${userProfile.userId}`);
                        };
                        return (
                          <Col>
                            <Card
                              className="burrow-userProfile-card"
                              onClick={navigateToProfile}
                              style={{ cursor: "pointer" }}
                            >
                              <Card.Img
                                variant="top"
                                src={
                                  userProfile.id ? userProfile.profileImageUrl : null
                                }
                              />
                              <Card.Body>
                                <Card.Title>
                                  {userProfile.id ? userProfile.handle : null}
                                </Card.Title>
                                <Card.Subtitle>
                                  {userProfile.id ? userProfile.firstName : null}{" "}
                                  {userProfile.id ? userProfile.lastName : null}
                                </Card.Subtitle>
                                <Card.Text>
                                  {userProfile.id ? userProfile.biography : null}
                                </Card.Text>
                              </Card.Body>
                            </Card>
                          </Col>
                        );
                      })
                    ) : (
                      <div></div>
                    )}
                  </Row>
                </Container>
              </div>
              </Tab>
          </Tabs>
        </Container>
      </Container>
    </div>
  );
}
