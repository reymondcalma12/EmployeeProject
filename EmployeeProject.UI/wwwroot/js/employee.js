$(document).ready(function () {
    let connection = new signalR.HubConnectionBuilder().withUrl("/adminHub").build();


    const currentYear = new Date().getFullYear();



    $("#employeeYear").on("input", function () {


        var years = $("#employeeYear").val();
        var year = parseInt(years);

        connection.invoke("GetUserMonthlyStatisticsEmployeeSort", year)
            .catch(function (err) {
                return console.error(err.toString());
            });

    });
    connection.on("GetUserMonthlyStatisticsEmployeeSort", function (statistics) {

        var container = $(".dashboardBodyDiv2Inner1Div2");

        if (statistics && statistics.length > 0) {

            statistics.forEach(function (user) {

                container.empty();

                let html = `
                        <div class="janNumber" style="${getBackgroundColor(user.january, user.low, user.med, user.max)};">${user.january}</div>
                        <div class="febNumber" style="${getBackgroundColor(user.february, user.low, user.med, user.max)};">${user.february}</div>
                        <div class="marNumber" style="${getBackgroundColor(user.march, user.low, user.med, user.max)};">${user.march}</div>
                        <div class="aprNumber" style="${getBackgroundColor(user.april, user.low, user.med, user.max)};">${user.april}</div>
                        <div class="mayNumber" style="${getBackgroundColor(user.may, user.low, user.med, user.max)};">${user.may}</div>
                        <div class="junNumber" style="${getBackgroundColor(user.june, user.low, user.med, user.max)};">${user.june}</div>
                        <div class="julNumber" style="${getBackgroundColor(user.july, user.low, user.med, user.max)};">${user.july}</div>
                        <div class="augNumber" style="${getBackgroundColor(user.august, user.low, user.med, user.max)};">${user.august}</div>
                        <div class="sepNumber" style="${getBackgroundColor(user.september, user.low, user.med, user.max)};">${user.september}</div>
                        <div class="octNumber" style="${getBackgroundColor(user.october, user.low, user.med, user.max)};">${user.october}</div>
                        <div class="novNumber" style="${getBackgroundColor(user.november, user.low, user.med, user.max)};">${user.november}</div>
                        <div class="decNumber" style="${getBackgroundColor(user.december, user.low, user.med, user.max)};">${user.december}</div>
                    <div class="yearlyTotalNumber">${user.totalHours}</div>
                `;

                container.append(html);

                $("#legendSectionDisplay").text(user.sectionName);


                $(".lowNumber").text(user.low);
                $(".medNumber").text(user.med);
                $(".maxNumber").text(user.max);

            });


        } else {

            container.empty();

            var htmls = `
                <div style="width: 100%; height: 100%; display:flex; justify-content:center; align-items:center;">
                    <h4>No Data</h4>
                </div>
             `;

            container.append(htmls);
        }
    });



    connection.on("EmployeeStatistics", function (statistics) {

        var container = $(".dashboardBodyDiv2Inner1Div2");

        if (statistics && statistics.length > 0) {

            statistics.forEach(function (user) {

                container.empty();

                let html = `
                        <div class="janNumber" style="${getBackgroundColor(user.january, user.low, user.med, user.max)};">${user.january}</div>
                        <div class="febNumber" style="${getBackgroundColor(user.february, user.low, user.med, user.max)};">${user.february}</div>
                        <div class="marNumber" style="${getBackgroundColor(user.march, user.low, user.med, user.max)};">${user.march}</div>
                        <div class="aprNumber" style="${getBackgroundColor(user.april, user.low, user.med, user.max)};">${user.april}</div>
                        <div class="mayNumber" style="${getBackgroundColor(user.may, user.low, user.med, user.max)};">${user.may}</div>
                        <div class="junNumber" style="${getBackgroundColor(user.june, user.low, user.med, user.max)};">${user.june}</div>
                        <div class="julNumber" style="${getBackgroundColor(user.july, user.low, user.med, user.max)};">${user.july}</div>
                        <div class="augNumber" style="${getBackgroundColor(user.august, user.low, user.med, user.max)};">${user.august}</div>
                        <div class="sepNumber" style="${getBackgroundColor(user.september, user.low, user.med, user.max)};">${user.september}</div>
                        <div class="octNumber" style="${getBackgroundColor(user.october, user.low, user.med, user.max)};">${user.october}</div>
                        <div class="novNumber" style="${getBackgroundColor(user.november, user.low, user.med, user.max)};">${user.november}</div>
                        <div class="decNumber" style="${getBackgroundColor(user.december, user.low, user.med, user.max)};">${user.december}</div>
                    <div class="yearlyTotalNumber">${user.totalHours}</div>
                `;

                container.append(html);

                $("#legendSectionDisplay").text(user.sectionName);


                $(".lowNumber").text(user.low);
                $(".medNumber").text(user.med);
                $(".maxNumber").text(user.max);

            });

          
        } else {

            container.empty();

            var htmls = `
                <div style="width: 100%; height: 100%; display:flex; justify-content:center; align-items:center;">
                    <h4>No Data</h4>
                </div>
             `;

            container.append(htmls);
        }
    });

    function getBackgroundColor(value, low, med, max) {

        if (low != 0 && med != 0 && max != 0) {
            if (value == 0) {
                return "background-color: white; color:black;";
            }
            else if (value <= low) {
                return "background-color: #D6AE0D; color:white;";
            } else if (value <= med) {
                return "background-color: #C3942C; color:white;";
            } else if (value <= max) {
                return "background-color: #A9C393; color:white;";
            } else {
                return "background-color: #E24848; color:white;";
            }
        }
        else {
            return "background-color: white; color:red;";
        }

    }



    connection.on("AllDataSheetEmployee", function (datas) {

        var container = $(".tbodyDataSheet");

        if (datas && datas.length > 0) {


            datas.forEach(function (data) {

                var startDate = new Date(data.startDate);
                var endDate = new Date(data.endDate);

                var formattedStartDate = startDate.toLocaleDateString("en-US", {
                    month: "long",
                    day: "numeric",
                    year: "numeric"
                });

                var formattedEndDate = endDate.toLocaleDateString("en-US", {
                    month: "long",
                    day: "numeric",
                    year: "numeric"
                });

                let html = `
                    <tr>
                        <td>${data.section.sectionName}</td>
                        <td>${data.appUser.fullName}</td>
                        <td>${data.project.projectName}</td>
                        <td>${data.activity.activityName}</td>
                        <td>${data.businessOrIt.businessOrItName}</td>
                        <td>${formattedStartDate}</td>
                        <td>${formattedEndDate}</td>
                        <td>${data.hoursPerDay}</td>
                        <td>${data.hoursPerMonth}</td>
                    </tr>
                `;

                container.append(html);

            });

        } else {
            container.empty();

            var htmls = `
                <div style="width: 97%; height: 55%; position: absolute;display:flex; justify-content:center; align-items:center;">
                    <h1>No Data</h1>
                </div>
             `;

            container.append(htmls);
        }

    });




    connection.on("SectionsDropdown", function (sections) {


        var userEditSection = $("#userEditSection");

        if (sections.length > 0) {

            sections.forEach(function (section) {

                let html = `<option value="${section.sectionId}">${section.sectionName}</option>`;

                userEditSection.append(html);
 


            });

        }
        else {

        }

    });



    connection.on("Holidays", function (holi) {

        var container = $(".tbodyHoliday").empty();

        holi.forEach(function (data) {


            var date = new Date(data.fixedDate);
            var fixedDate = date.toLocaleDateString("en-US", {
                month: "long",
                day: "numeric"
            });

            let html = `
                 <tr>
                    <td style="font-weight:bold;">${data.fixedName}</td>
                    <td>${fixedDate}</td>
                </tr>

                `;

            container.append(html);
        });
    });


    function GetSectionsDropdown() {
        connection.invoke("GetSections")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }



    function GetAllDataSheetEmployee() {
        connection.invoke("GetAllDataSheetEmployee")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }


    function GetStatistics() {
        connection.invoke("GetStatisticsEmployee")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }



    function GetReadyHoliday(year) {

        connection.invoke("GetHolidays", currentYear)
            .catch(function (err) {
                return console.error(err.toString());
            });
    }

    function GetHoliday() {
        connection.invoke("GetAllHolidays")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }


    function GetUserMonthlyStatisticsEmployeeSort(year) {

        connection.invoke("GetUserMonthlyStatisticsEmployeeSort", year)
            .catch(function (err) {
                return console.error(err.toString());
            });
    }

 

    connection.start().then(function () {

        const currentYear = new Date().getFullYear();

        GetStatistics();
        GetAllDataSheetEmployee();
        GetSectionsDropdown();

        GetUserMonthlyStatisticsEmployeeSort(currentYear);

        GetHoliday();

    }).catch(function (err) {
        console.error(err.toString());
    });
});