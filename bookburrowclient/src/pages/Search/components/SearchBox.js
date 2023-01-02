import React from 'react';

export default function SearchBox({searchValue, setSearchValue}) {
  return (
    <div className='col col-sum-4'>
        <input 
        className='form-control' 
        value={searchValue}
        onChange={(event) => setSearchValue(event.target.value)}
        placeholder='Search Book Burrow'
        ></input>
    </div>
  )
}