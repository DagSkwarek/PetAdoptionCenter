import React, { useEffect, useState } from 'react';
import { Link, useParams } from 'react-router-dom';
import MyCalendar from '../../../Components/BigCalendarActivity/CalendarActivity';
import { fetchCalendarDataForPet } from '../../../Service/fetchCalendarDataForPet';
import { fetchDataForPet } from '../../../Service/fetchDataForPet';
import { DateTimePicker, LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { address_url } from '../../../Service/url';
import axios from 'axios';
import Modal from 'react-modal';
import './PetById.css';
import GenderPetLabel from '../../../Components/Enum/GenderPetLabel';
import SizePetLabel from '../../../Components/Enum/SizePetLabel';
import StatusPetLabel from '../../../Components/Enum/StatusPetLabel';
import FlipCardAvailable from '../../../Components/FlipCardAvailable';
import { fetchDataForShelter } from '../../../Service/fetchDataForShelter';
import { FetchDataForAdoption } from '../../../Service/FetchDataForAdoption';
import { useUser } from '../../../Components/UserContext';
import { FaSpinner } from 'react-icons/fa';

const PetById = ({
  petId,
  petAdoptionId,
  userAdoptionId,
  adoptionById,
  petProfileId,
  petsTempHouseId,
  petTempHouseId,
  userTempHouseId,
  tempHouseId
}) => {
  console.log(petsTempHouseId);
  const { id } = useParams();
  const { user, setUser } = useUser();
  const [calendarData, setCalendarData] = useState([]);
  const [selectedActivity, setSelectedActivity] = useState(null);
  const [startDate, setStartDate] = useState(Date.now());
  const [endDate, setEndDate] = useState(Date.now());
  const [activityName, setActivityName] = useState('');
  const [visible, setVisible] = useState(false);
  const [edit, setEdit] = useState(false);
  const [showCalendar, setShowCalendar] = useState(false);
  const [petDataVisible, setPetDataVisible] = useState(true);
  const [choosenMeeting, setChoosenMeeting] = useState([]);
  const [petData, setPetData] = useState({});
  const [shelterData, setShelterData] = useState({});
  const [shelterAddress, setShelterAddress] = useState({
    street: '',
    number: '',
    city: '',
  });
  const [activityAdded, setActivityAdded] = useState(false);
  const [meetingsSuccessMessage, setMeetingsSuccessMessage] = useState(null);
  const [retryMeetings, setRetryMeetings] = useState(false);
  const [adoptionData, setAdoptionData] = useState({});
  const [loading, setLoading] = useState(false);

  Modal.setAppElement('#root');

  const customStyles = {
    content: {
      top: '50%',
      left: '50%',
      right: 'auto',
      bottom: 'auto',
      marginRight: '-50%',
      transform: 'translate(-50%, -50%)',
      zIndex: 4,
    },
  };
  useEffect(() => {
    if (id) {
      fetchData(id);
    }
  }, [id]);

  useEffect(() => {
    if (petId) {
      fetchData(petId);
    }
  }, [petId]);

  useEffect(() => {
    if (petProfileId) {
      fetchData(petProfileId);
    }
  }, [petProfileId]);

  useEffect(() => {
    if (petAdoptionId) {
      fetchData(petAdoptionId);
    }
  }, [petAdoptionId]);

  useEffect(() => {
    if (petsTempHouseId) {
      fetchData(petsTempHouseId);
    }
  }, [petsTempHouseId]);

  useEffect(() => {
    if (petTempHouseId) {
      fetchData(petTempHouseId);
    }
  }, [petsTempHouseId]);
  useEffect(() => {
    if (adoptionById) {
      fetchDataAdoption();
    }
  }, [adoptionById]);

  const fetchDataAdoption = async () => {
    if (adoptionById) {
      try {
        const adoptionResponseData = await FetchDataForAdoption(adoptionById);
        setAdoptionData(adoptionResponseData);
      } catch (error) {
        console.error('Adoption download error:', error);
      }
    }
  };

  const fetchData = async (param) => {
    try {
      const calendarData = await fetchCalendarDataForPet(param);
      setCalendarData(calendarData.Activities);

      const petDataById = await fetchDataForPet(param);
      setPetData(petDataById);
      // console.log(petDataById);

      if (petDataById && petDataById.ShelterId) {
        const shelterDataById = await fetchDataForShelter(
          petDataById.ShelterId
        );
        setShelterData(shelterDataById);
        //console.log(shelterDataById);
        //console.log(shelterDataById.Name);

        if (shelterDataById) {
          setShelterAddress({
            street: shelterDataById.ShelterAddress.Street,
            number: shelterDataById.ShelterAddress.FlatNumber
              ? `${shelterDataById.ShelterAddress.HouseNumber}/${shelterDataById.ShelterAddress.FlatNumber}`
              : shelterDataById.ShelterAddress.HouseNumber,
            city: shelterDataById.ShelterAddress.City,
          });
        }
      }
    } catch (error) {
      console.log('shelter fetch error: ' + error);
    }
  };
  //console.log(petData);
  const updateEndDate = (e) => {
    const date = new Date(e);
    const iso = date.toISOString();
    setEndDate(iso);
  };
  const updateStartDate = (e) => {
    const date = new Date(e);
    const iso = date.toISOString();
    setStartDate(iso);
  };
  const handleSubmit = async () => {
    try {
      const resp = await axios.post(
        `${address_url}/Shelters/${petData.ShelterId}/pets/${id}/calendar/activities`,
        {
          Name: activityName,
          StartActivityDate: startDate,
          EndActivityDate: endDate,
        }
      );
      setCalendarData(resp.data);
      console.log('calendar data', resp.data);
    } catch (err) {
      console.log(err);
    }
  };
  const handleEventClick = (event) => {
    setSelectedActivity(event);
    //console.log(event);
    setVisible(true);
  };
  const goToPetCalendar = () => {
    setShowCalendar(true);
    setPetDataVisible(false);
  };
  const updateActivity = async () => {
    try {
      const resp = await axios.put(
        `${address_url}/Shelters/${petData.ShelterId}/pets/${id}/calendar/activities/${selectedActivity.id}`,
        {
          Name: activityName,
          StartActivityDate: startDate,
          EndActivityDate: endDate,
        }
      );
      console.log(resp);
    } catch (err) {
      console.log(err);
    }
  };
  const RemoveActivity = async () => {
    try {
      const resp = await axios.delete(
        `${address_url}/Shelters/${petData.ShelterId}/pets/${id}/calendar/activities/${selectedActivity.id}`
      );
      console.log(resp);
    } catch (err) {
      console.log(err);
    }
  };
  const handleMeetForAdoption = async () => {
    setLoading(true);
    try {
      const response = await axios.post(
        `${address_url}/Shelters/${petData.ShelterId}/pets/${petAdoptionId}/calendar/activities/${selectedActivity.id}/users/${userAdoptionId}/adoptions/${adoptionById}/meetings-adoption`
      );
      setChoosenMeeting(response.data);
      //console.log('meetforadoption', response.data);
      setActivityAdded(true);
      setMeetingsSuccessMessage('Meeting added successfully!');
      setRetryMeetings(false);
    } catch (error) {
      console.error(error);
      setActivityAdded(false);
      setMeetingsSuccessMessage('Failed to add meeting. Please try again.');
      setRetryMeetings(true);
      setLoading(false);
    }
    setTimeout(() => {
      setLoading(false);
    }, 2000);
  };

  const handleMeetForTempHouse = async () => {
    setLoading(true);
    try {
      const response = await axios.post(
        `${address_url}/Shelters/temporary-houses/${tempHouseId}/pets/${petTempHouseId}/calendar/activities/${selectedActivity.id}/users/meetings-temporary-house`
      ); 
      setChoosenMeeting(response.data);
      //console.log('meetforadoption', response.data);
      setActivityAdded(true);
      setMeetingsSuccessMessage('Meeting added successfully!');
      setRetryMeetings(false);
    } catch (error) {
      console.error(error);
      setActivityAdded(false);
      setMeetingsSuccessMessage('Failed to add meeting. Please try again.');
      setRetryMeetings(true);
      setLoading(false);
    }
    setTimeout(() => {
      setLoading(false);
    }, 2000);
  };

  return (
    <div className="pet-by-id-container-all">
      {petData && petData.BasicHealthInfo && petDataVisible ? (
        <>
          <div className="pet-by-id-container">
            <div className="img-and-info">
              <div className="botton-pet-by-id">
                {id && user.id && petData.AvaibleForAdoption ? (
                  <Link
                    to={`/Shelters/adoptions/pets/${id}/users/${user.id}/preadoption-poll`}
                    className="find-pet"
                  >
                    Adopt Me
                  </Link>
                ) : petData.AvaibleForAdoption ? (
                  <Link
                    to={`/Shelters/adoptions/pets/${id}`}
                    className="find-pet"
                  >
                    Adopt Me
                  </Link>
                ) : null}
                {id && user.id && petData.AvaibleForAdoption &&  petData.Status !== 0 && petData.Status !== 5 ? (
                  <Link
                    to={`/Shelters/${shelterData.Id}/temporaryHouses/pets/${id}/users/${user.id}/pre-temporary-house-poll`}
                    className="find-pet"
                  >
                    Give me a temporary house
                  </Link>
                ) : petData.AvaibleForAdoption && petData.Status !== 0 && petData.Status !== 5 ? (
                  <Link
                    className="find-pet"
                    to={`/Shelters/${shelterData.Id}/temporaryHouses/pets/${id}`}
                  >
                    Give me a temporary house
                  </Link>
                ) : null}

                {petData.Status !== 3 &&
                  !petId &&
                  !petAdoptionId &&
                  !petProfileId &&
                  !petTempHouseId &&
                  !petsTempHouseId &&
                  petData.Status !== 0 &&
                  petData.Status !== 5 && (
                    <>
                      {' '}
                      <Link
                    to={`/Shelters/${shelterData.Id}/pets/${id}/users/adopt-me-virtually`}
                    className="find-pet"
                  >Adopt me virtually</Link>
                      <button className="pet-button" onClick={goToPetCalendar}>
                        Take me for a walk
                      </button>
                      <button className="pet-button" onClick={goToPetCalendar}>
                        Make me visit at shelter
                      </button>
                    </>
                  )}
              </div>
              <div className="pet-by-id-card-image-name">
                <img
                  className="pet-img"
                  src={`data:image/jpeg;base64, ${petData.ImageBase64}`}
                  alt=""
                />
                <img
                  src={process.env.PUBLIC_URL + '/Photo/whitePaw.png'}
                  alt="Lapka"
                  className="paw-icon"
                />
                <h2>{petData.BasicHealthInfo.Name}</h2>
              </div>
              <div className="more-info-pet-by-id">
                <p>
                  <span className="small-font">Age: </span>
                  <span className="large-font">
                    {petData.BasicHealthInfo.Age}
                  </span>
                </p>

                <p>
                  <span className="small-font">Size: </span>
                  <span className="large-font">
                    {SizePetLabel(petData.BasicHealthInfo.Size)}
                  </span>
                </p>

                <p>
                  <span className="small-font">Gender: </span>
                  <span className="large-font">
                    {GenderPetLabel(petData.Gender)}
                  </span>
                </p>

                <p>
                  <span className="small-font">Status: </span>
                  <span className="large-font">
                    {StatusPetLabel(petData.Status)}
                  </span>
                </p>

                {shelterData && shelterData.Name && (
                  <p>
                    <span className="small-font">Shelter name: </span>
                    <span className="large-font">{shelterData.Name}</span>
                  </p>
                )}
                <span className="small-font">Shelter Address: </span>
                <p className="address-shelter">
                  <span className="small-font">street: </span>
                  <span className="large-font">{shelterAddress.street}</span>
                </p>
                <p className="address-shelter">
                  <span className="small-font">number: </span>
                  <span className="large-font">{shelterAddress.number}</span>
                </p>
                <p className="address-shelter">
                  <span className="small-font">city: </span>
                  <span className="large-font">{shelterAddress.city}</span>
                </p>
                <p>
                  <span className="small-font">
                    Is available for adoption:{' '}
                  </span>
                  <span className="large-font">
                    {petData.AvaibleForAdoption ? 'Yes' : 'No'}
                  </span>
                </p>
                <p>
                  <span className="small-font">Vaccinations: </span>
                  <span className="large-font">
                    {petData.BasicHealthInfo &&
                    petData.BasicHealthInfo.Vaccinations &&
                    petData.BasicHealthInfo.Vaccinations.length > 0
                      ? petData.BasicHealthInfo.Vaccinations.map(
                          (vac) => vac.VaccinationName
                        ).join(', ')
                      : 'No vaccinations available'}
                  </span>
                </p>
                <p>
                  <span className="small-font">Diseases: </span>
                  <span className="large-font">
                    {petData.BasicHealthInfo &&
                    petData.BasicHealthInfo.MedicalHistory &&
                    petData.BasicHealthInfo.MedicalHistory.length > 0
                      ? petData.BasicHealthInfo.MedicalHistory.map(
                          (vac) => vac.NameOfdisease
                        ).join(', ')
                      : 'No diseases'}
                  </span>
                </p>
              </div>
            </div>
            <div className="description-pet-by-id">
              <p>
                <span className="small-font">Description: </span>
                <span className="large-font">{petData.Description}</span>
              </p>
            </div>
          </div>
          {petData.Status !== 4 && petData.Status !== 3 && petData.Status !== 5 && petData.Status !== 0 && (
            <div className="pets-available-to-adoption">
              <div className="pet-inscription">
                <h2>Pets available for adoption</h2>
              </div>
              <div className="pet-card">
                <FlipCardAvailable />
              </div>
            </div>
          )}
        </>
      ) : null}
      {(petData &&
      petAdoptionId &&
      petDataVisible &&
      !adoptionData.IsMeetings) || (petData && petTempHouseId && petDataVisible) ? (
        <div className="meetings-button-pet-adoption">
          <h2>Small Palls Pet's Calendar</h2>
          {(petAdoptionId || petTempHouseId) && (
            <h2 className="important">
              Click on the meeting ("KNOW ME") to add it.
            </h2>
          )}
          <Modal
            isOpen={visible}
            onRequestClose={() => setVisible(false)}
            style={customStyles}
          >
            {edit ? (
              <div className="modal-content">
                <div className="activity-form">
                  Title:{' '}
                  <input
                    onChange={(e) => setActivityName(e.target.value)}
                    type="text"
                  ></input>
                  Start Date:{' '}
                  <DateTimePicker onChange={(e) => updateStartDate(e)} />
                  End Date:{' '}
                  <DateTimePicker onChange={(e) => updateEndDate(e)} />
                  <button onClick={updateActivity}>done</button>
                  <button onClick={() => setEdit(false)}>go back</button>
                </div>
              </div>
            ) : (
              <div className="edit-remove-meet-buttons">
                {!petAdoptionId && !petTempHouseId ? (
                  <>
                    <button onClick={() => setEdit(true)}>Edit</button>
                    <button onClick={() => RemoveActivity}>Delete</button>
                  </>
                ) : null}
                {petAdoptionId && !activityAdded ? (
                  <div className="spinner-container">
                    <button onClick={handleMeetForAdoption}>
                      {loading ? (
                        <FaSpinner className="spinner" />
                      ) : (
                        'Add meet'
                      )}
                    </button>
                  </div>
                ) : null}
                    {petTempHouseId && !activityAdded ? (
                  <div className="spinner-container">
                    <button onClick={handleMeetForTempHouse}>
                      {loading ? (
                        <FaSpinner className="spinner" />
                      ) : (
                        'Add meet'
                      )}
                    </button>
                  </div>
                ) : null}
              </div>
            )}
            {petAdoptionId && activityAdded ? (
              <div className="adoption-success-message">
                <p>{meetingsSuccessMessage}</p>
                {retryMeetings ? (
                  <button onClick={handleMeetForAdoption}>Try Again</button>
                ) : (
                  <Link
                    className="button-link-go-back"
                    to={`/Shelters/adoptions/pets/users/${userAdoptionId}`}
                  >
                    Go back
                  </Link>
                )}
              </div>
            ) : null}
                {petTempHouseId && activityAdded ? (
              <div className="adoption-success-message">
                <p>{meetingsSuccessMessage}</p>
                {retryMeetings ? (
                  <button onClick={handleMeetForTempHouse}>Try Again</button>
                ) : (
                  <Link
                    className="button-link-go-back"
                    to={`/Shelters/temporaryHouses/${tempHouseId}/pets/users/${userTempHouseId}`}
                  >
                    Go back
                  </Link>
                )}
              </div>
            ) : null}
          </Modal>
          <MyCalendar events={calendarData} onEventClick={handleEventClick} />
        </div>
      ) : null}
      {console.log(adoptionData.Activity)}
      {petAdoptionId &&
        adoptionById &&
        adoptionData.Activity &&
        console.log(adoptionData.Activity.Activities)}
      {showCalendar && (
        <LocalizationProvider dateAdapter={AdapterDayjs}>
          <Modal
            isOpen={visible}
            onRequestClose={() => setVisible(false)}
            style={customStyles}
          >
            {edit ? (
              <div className="modal-content">
                <div className="activity-form">
                  Title:{' '}
                  <input
                    onChange={(e) => setActivityName(e.target.value)}
                    type="text"
                  ></input>
                  Start Date:{' '}
                  <DateTimePicker onChange={(e) => updateStartDate(e)} />
                  End Date:{' '}
                  <DateTimePicker onChange={(e) => updateEndDate(e)} />
                  <button onClick={updateActivity}>done</button>
                  <button onClick={() => setEdit(false)}>go back</button>
                </div>
              </div>
            ) : (
              <div className="edit-remove-meet-buttons">
                {!petAdoptionId && !petTempHouseId && (
                  <>
                    <button onClick={() => setEdit(true)}>Edit</button>
                    <button onClick={() => RemoveActivity}>Delete</button>
                  </>
                )}
              </div>
            )}
          </Modal>
          <MyCalendar events={calendarData} onEventClick={handleEventClick} />
          {!petAdoptionId && !petTempHouseId && (
            <div className="add-activity-container">
              <h3>Add activity!</h3>
              <form className="activity-form">
                Name:{' '}
                <input
                  type="text"
                  onChange={(e) => setActivityName(e.target.value)}
                ></input>
                Start Date:{' '}
                <DateTimePicker onChange={(e) => updateStartDate(e)} />
                End Date: <DateTimePicker onChange={(e) => updateEndDate(e)} />
                <button onClick={handleSubmit}>Add</button>
              </form>
            </div>
          )}
        </LocalizationProvider>
      )}
    </div>
  );
};

export default PetById;
