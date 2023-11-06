import React, { useState, useEffect } from 'react';
import { Link, useParams } from 'react-router-dom';
import { address_url } from '../Service/url';
import axios from 'axios';
import MyCalendar from '../Components/BigCalendarActivity/CalendarActivity';
import PetById from '../Pages/Pets/PetsById/PetById';
import './Meetings.css';

const Meetings = () => {
  const { userId } = useParams();
  console.log(userId);
  const [userData, setUserData] = useState({});

  useEffect(() => {
    const fetchProfileData = async () => {
      try {
        const response = await axios.get(`${address_url}/Users/${userId}`, {
          headers: {
            Authorization: `Bearer ${userId.token}`,
          },
        });
        setUserData(response.data);
        console.log(response.data.Adoptions);
      } catch (err) {
        console.log(err);
      }
    };

    fetchProfileData();
  }, [userId.id, userId.token]);

  return (
    <div  className="adoption-main-page-container">
      {userId && userData.Adoptions?.length >= 1 && (
        <>
          {console.log(userData.Adoptions)}
          <h2 className="adoption-main-page">Your Adoptions</h2>
          {userData.Adoptions?.map((adoption) => (
            <div key={adoption.Id} className="adoption-card-meetings">
              <PetById
                petId={adoption.PetId}
                userId={adoption.UserId}
                adoptionId={adoption.Id}
                calendarAdoptionId={adoption.CalendarId}
              />
              <h3 className="adoption-main-page">
                Status:{' '}
                {adoption.IsContractAdoption ? 'Contracted' : 'Not Contracted'}
              </h3>
              <Link
                to={`/Shelters/adoptions/${adoption.Id}/pets/${adoption.PetId}/users/${adoption.UserId}`}
                className="adoption-main-page-link">
                Show more
              </Link>
              {!adoption.IsMeetings && (
                <MyCalendar events={adoption.Activity.Activities}  className="adoption-main-page-calendar"/>
              )}
              {adoption.Activity.Activities?.length >= 1 &&
                adoption.Activity.Activities.every(
                  (a) => new Date(a.EndActivityDate) < new Date()
                ) &&
                !adoption.IsMeetings &&
                !adoption.IsContractAdoption && (
                  <Link
                    to={`/Shelters/adoptions/${adoption.Id}/pets/${adoption.PetId}/users/${adoption.UserId}`}
                    className="adoption-main-page-link">
                    Confirm your adoption
                  </Link>
                )}
              {adoption.IsMeetings && !adoption.IsContractAdoption && (
                <Link
                  to={`/Shelters/adoptions/${adoption.Id}/pets/${adoption.PetId}/users/${adoption.UserId}`}
                  className="adoption-main-page-link">
                  Contract adoption
                </Link>
              )}
            </div>
          ))}
        </>
      )}
    </div>
  );
};

export default Meetings;