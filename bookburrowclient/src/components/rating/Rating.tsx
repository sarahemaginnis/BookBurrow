import * as React from 'react';
import Rating from '@mui/material/Rating';
import Box from '@mui/material/Box';
import StarIcon from '@mui/icons-material/Star';

const labels: { [index: string]: string } = {
  0.5: 'Did not like it',
  1: 'Did not like it+',
  1.5: 'It was okay',
  2: 'It was okay+',
  2.5: 'Liked it',
  3: 'Liked it+',
  3.5: 'Really liked it',
  4: 'Really liked it+',
  4.5: 'It was amazing',
  5: 'It was amazing+',
};

function getLabelText(value: number) {
  return `${value} Star${value !== 1 ? 's' : ''}, ${labels[value]}`;
}

export default function HoverRating(props) {
  const [value, setValue] = React.useState<number | null>(props.value);
  const [hover, setHover] = React.useState(-1);

  return (
    <Box
      sx={{
        width: 200,
        display: 'flex',
        alignItems: 'center',
      }}
    >
      <Rating
        name="hover-feedback"
        value={value}
        precision={0.5}
        getLabelText={getLabelText}
        size="large"
        onChange={(event, newValue) => {
          setValue(newValue);
          props.func(newValue);
          props.id(newValue * 2);
        }}
        onChangeActive={(event, newHover) => {
          setHover(newHover);
        }}
        emptyIcon={<StarIcon style={{ opacity: 0.55 }} fontSize="inherit" />}
      />
      {value !== null && (
        <Box sx={{ ml: 2 }}>{labels[hover !== -1 ? hover : value]}</Box>
      )}
    </Box>
  );
}
