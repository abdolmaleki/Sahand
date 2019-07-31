using Application.AtlasjetService;
using Repository.Models.Entity;
using Repository.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    /// <summary>
    /// Convert Atlasjet Passanger data to AddPassangerViewModel viewModel
    /// </summary>
    public class PassangerMapper
    {
        public static PassengersData[] ConvertPassengerViewModelToArray(AddPassangerViewModel viewModel)
        {
            int passangerCount = viewModel.Passangers.Count;
            PassengersData[] PassangerArray = new PassengersData[passangerCount];

            PassengerSeatData[] seatData = new PassengerSeatData[1];
            seatData[0].seatNumber = 1;
            seatData[0].destination = "ARMS";
            seatData[0].flightdate = "";
            seatData[0].origin = "Y";
            seatData[0].seatLabel = "WCHC";
            seatData[0].seatLabel = "8";

            PassengerSSRData[] passengerSSRDatas = new PassengerSSRData[1];
            passengerSSRDatas[0] = new PassengerSSRData();
            passengerSSRDatas[0].segno = 1;
            passengerSSRDatas[0].ssrnote = "ARMS";
            passengerSSRDatas[0].ssrno = 14;
            passengerSSRDatas[0].ssrtext = "Y";
            passengerSSRDatas[0].ssrcode = "WCHC";
            passengerSSRDatas[0].ssravail = "8";

            for (int i = 0; i < passangerCount; i++)
            {

                Passanger passanger = viewModel.Passangers[i];
                PassengersData passangerData = new PassengersData
                {
                    birthDate = passanger.BirthDay_Day + " " + passanger.BirthDay_Month + " " + passanger.BirthDay_Year,
                    countryCode = "98",
                    email = passanger.Email,
                    firstName = passanger.EFirstName,
                    gender = (passanger.Gender == 1) ? "E" : "B",
                    identityNo = passanger.NationalID,
                    jetmilCardNo = "",
                    lastName = passanger.ELastNAme,
                    order = "",
                    passaportNo = viewModel.Passport.PassportID,
                    passengerType = passanger.Type,
                    phoneArea = "21",
                    phoneNumber = passanger.Tell,
                    popdApprove = "1",
                    psgrseatdata = seatData,
                    psgrssrdata = passengerSSRDatas

                };

                PassangerArray[i] = passangerData;
            }

            return PassangerArray;
        }
    }
}
