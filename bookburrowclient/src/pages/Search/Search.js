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
}) {
  const [key, setKey] = useState('books');
  const navigate = useNavigate();

  return (
    <div>
      <Container>
        <h1>Search</h1>
        <SearchBox
          searchValue={searchBookValue}
          setSearchValue={setSearchBookValue}
        />
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
                              <Card.Footer></Card.Footer>
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
            <Tab eventKey="posts" title="Posts"></Tab>
            <Tab eventKey="people" title="People"></Tab>
          </Tabs>
        </Container>
      </Container>
    </div>
  );
}
