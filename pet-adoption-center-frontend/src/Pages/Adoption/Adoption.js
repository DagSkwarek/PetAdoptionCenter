import React, { useEffect, useState } from 'react';
import './Adoption.css';
import FlipCardAdopted from '../../Components/FlipCardAdopted';
import FlipCardAvailable from '../../Components/FlipCardAvailable';
import { Link, useParams } from 'react-router-dom';
import { useUser } from '../../Components/UserContext';
import axios from 'axios';
import { address_url } from '../../Service/url';
import PreadoptionPoll from '../../Components/PreadoptionPoll';
import PetById from '../Pets/PetsById/PetById.js';
import MyCalendar from '../../Components/BigCalendarActivity/CalendarActivity';
import PreadoptionPollInfo from '../../Components/PreadoptionPollInfo';
import MeetingsInfo from '../../Components/MeetingsInfo';
import ContractAdoptionInfo from '../../Components/ContractAdoptionInfo';
import ContractAdoption from '../../Components/ContractAdoption';
import { fetchDataForPet } from '../../Service/fetchDataForPet.js';

const Adoption = ({ petData, setPetData }) => {
  const { id } = useParams();
  console.log(id);
  const { user, setUser } = useUser();
  const [userData, setUserData] = useState([]);
  const [preadoptionPollVisible, setPreadoptionPollVisible] = useState(false);
  const [meetingsVisible, setMeettingsVisible] = useState(false);
  const [contractAdoptionVisible, setContractAdoptionVisible] = useState(false);
  const [calendarAdoptionVisible, setCalendarAdoptionVisible] = useState(false);
  const [signContractAdoptionVisible, setSignContractAdoptionVisible] =
    useState(false);
  const [selectedPetId, setSelectedPetId] = useState(null);
  const [selectedAdoptionId, setSelectedAdoptionId] = useState(null);
  const [selectedPetData, setSelectedPetData] = useState({});

  useEffect(() => {
    const fetchProfileData = async () => {
      try {
        const response = await axios.get(`${address_url}/Users/${user.id}`, {
          headers: {
            Authorization: `Bearer ${user.token}`,
          },
        });
        setUserData(response.data);
        // console.log(response.data.Adoptions);
      } catch (err) {
        console.log(err);
      }
    };

    fetchProfileData();
  }, [user.id, user.token]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const responseData = await fetchDataForPet(id);
        setSelectedPetData(responseData);
        console.log(responseData.ShelterId);
      } catch (err) {
        console.log('shelter fetch error: ' + err);
      }
    };
    fetchData();
  }, []);

  const handleConfirmAdoption = async (adoptionId) => {
    try {
      const resp = await axios.post(
        `${address_url}/Shelters/adoptions/${adoptionId}/meetings-adoption-done`
      );
      console.log(resp);
    } catch (err) {
      console.log(err);
    }
  };
  // console.log(userData.Adoptions);
  //console.log(petData.Id);
  // console.log(petData.ShelterId);
  const handleSignContract = async () => {
    setSignContractAdoptionVisible(true);
  };
  const showPreadoptionPoll = async () => {
    setPreadoptionPollVisible(true);
  };

  const hidePreadoptionPoll = async () => {
    setPreadoptionPollVisible(false);
  };
  const showInfoMeetings = async () => {
    setMeettingsVisible(true);
  };

  const hideInfoMeetings = () => {
    setMeettingsVisible(false);
  };
  const showContractAdoption = async () => {
    setContractAdoptionVisible(true);
  };

  const hideContractAdoption = async () => {
    setContractAdoptionVisible(false);
  };

  return (
    <div>
      <div className="adoption-container">
        {preadoptionPollVisible ? (
          <div className="preadoption-poll">
            <PreadoptionPollInfo />
            <button
              className="close-preadoption-poll"
              onClick={hidePreadoptionPoll}
            >
              Close Preadoption Poll
            </button>
          </div>
        ) : meetingsVisible ? (
          <div className="meetings">
            {user.id && userData.Adoptions?.length >= 1 ? (
              <>
                {console.log(userData.Adoptions)}
                <h2>Your Adoptions</h2>
                {userData.Adoptions?.map((adoption) => (
                  <div key={adoption.Id} className="adoption-card">
                    <PetById
                      petId={adoption.PetId}
                      userId={adoption.UserId}
                      adoptionId={adoption.Id}
                      calendarAdoptionId={adoption.CalendarId}
                    />
                    <h3>
                      Calendar Id:
                      {adoption.CalendarId}
                    </h3>
                    <h3>
                      Adoption Id:
                      {adoption.Id}
                    </h3>
                    <h3>
                      Pet Id:
                      {adoption.PetId}
                    </h3>
                    <h3>
                      Status:{' '}
                      {adoption.IsContractAdoption
                        ? 'Contracted'
                        : 'Not Contracted'}
                    </h3>
                    <h3>
                      User Id:
                      {adoption.UserId}
                    </h3>
                    <MyCalendar events={adoption.Activity.Activities} />
                    {adoption.Activity.Activities?.length >= 1 &&
                      adoption.Activity.Activities.every(
                        (a) => new Date(a.EndActivityDate) < new Date()
                      ) && (
                        <button
                          className="confirm-your-choose"
                          onClick={() => handleConfirmAdoption(adoption.Id)}
                        >
                          Confirm your adoption
                        </button>
                      )}
                  </div>
                ))}
              </>
            ) : (
              <MeetingsInfo />
            )}
            <button onClick={hideInfoMeetings}>Close Info Meetings</button>
          </div>
        ) : contractAdoptionVisible ? (
          <div className="contract-adoption">
            {user.id && userData.Adoptions?.length >= 1 ? (
              <>
                <h2>Your Adoptions</h2>
                {userData.Adoptions?.map((adoption) => (
                  <div key={adoption.PetId} className="adoption-card">
                    <PetById
                      petId={adoption.PetId}
                      userId={adoption.UserId}
                      adoptionId={adoption.Id}
                      // onAdoptMeClick={handleAdoptMeClick}
                    />
                    <h3>
                      Calendar Id:
                      {adoption.CalendarId}
                    </h3>
                    <h3>
                      Adoption Id:
                      {adoption.Id}
                    </h3>
                    <h3>
                      Pet Id:
                      {adoption.PetId}
                    </h3>
                    <h3>
                      Status:{' '}
                      {adoption.IsContractAdoption
                        ? 'Contracted'
                        : 'Not Contracted'}
                    </h3>
                    {adoption.IsMeetings && (
                      <>
                        <button
                          className="sign-adoption-contract"
                          onClick={handleSignContract}
                        >
                          Adoption contract
                        </button>
                        {signContractAdoptionVisible && (
                          <ContractAdoption
                            petData={petData}
                            setPetData={setPetData}
                            petId={adoption.PetId}
                            userId={adoption.UserId}
                            adoptionId={adoption.Id}
                            isMeetings={adoption.IsMeetings}
                          />
                        )}
                      </>
                    )}
                  </div>
                ))}
              </>
            ) : (
              <ContractAdoptionInfo />
            )}
            <button onClick={hideContractAdoption}>
              Close Contract Adoption
            </button>
          </div>
        ) : (
          <>
            <div className="adoption-button-card">
              <div className="button-more-info">
                {user.id && id && selectedPetData ? (
                  <Link to={`/Shelters/adoptions/pets/${id}/users/${user.id}`}>
                    Preadoption Poll
                  </Link>
                ) : (
                  <div className="button-more-info">
                    <button
                      className="button-adoption"
                      onClick={showPreadoptionPoll}
                    >
                      Preadoption Poll
                    </button>
                  </div>
                )}

                <button className="button-adoption" onClick={showInfoMeetings}>
                  Meetings to know your pet
                </button>
                <button
                  className="button-adoption"
                  onClick={showContractAdoption}
                >
                  Contract adoption
                </button>
                <Link to={`/Users/pets`} className="find-pet">
                  Find your new best friend
                </Link>
              </div>
              <div className="adoption-card-page-adoption">
                <div className="pet-inscription">
                  <h2 className="title-pet">Adopted pets</h2>
                </div>
                <FlipCardAdopted />
              </div>
            </div>
            <div className="petsAvailableForAdoption">
              <div className="pet-inscription">
                <h2 className="title-pet">Pets available for adoption</h2>
              </div>
              <div className="pet-card">
                <FlipCardAvailable />
              </div>
            </div>
          </>
        )}
      </div>
    </div>
  );
};

export default Adoption;