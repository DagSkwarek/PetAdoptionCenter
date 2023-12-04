import './FlipCard.css';
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './FlipCard.css';
import GenericCard from './GenericCard';
import { address_url } from '../Service/url';

const FlipCardShelterPets = ({ id }) => {
  const [petsAvailable, setPetsAvailable] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [petsPerPage] = useState(3);

  useEffect(() => {
    GetAvailablePetsForAdoption();
  }, []);

  async function GetAvailablePetsForAdoption() {
    try {
      const response = await axios.get(
        `${address_url}/Shelters/${id}/pets/avaible`
      );
      setPetsAvailable(response.data);
    } catch (error) {
      console.error(error);
    }
  }

  const indexOfLastPet = currentPage * petsPerPage;
  const indexOfFirstPet = indexOfLastPet - petsPerPage;
  const currentPets = petsAvailable.slice(indexOfFirstPet, indexOfLastPet);

  return (
    <div>
      <div className="card-container">
        {currentPets.map((pet, index) => (
          <GenericCard key={index} pet={pet} />
        ))}
      </div>
      <div className="pagination">
        <button
          onClick={() => setCurrentPage(currentPage - 1)}
          disabled={currentPage === 1}
          className="button-pagination"
        >
          Previous
        </button>
        <button
          onClick={() => setCurrentPage(currentPage + 1)}
          disabled={indexOfLastPet >= petsAvailable.length}
          className="button-pagination"
        >
          Next
        </button>
      </div>
    </div>
  );
};

export default FlipCardShelterPets;
