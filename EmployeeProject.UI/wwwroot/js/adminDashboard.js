$(document).ready(function () {

    let connection = new signalR.HubConnectionBuilder().withUrl("/adminHub").build();

    const currentYear = new Date().getFullYear();

    let projectManagerProjectId = 0;

    let sectionLegends = [];

    $("#userInputPassword").on("focusout", function () {
        var pass = $("#userInputPassword").val();

        if (pass.length == 0) {

        }
        else {
            if (pass.length < 8) {
                alert("Password Must Be 8 Digit!")
            } 
            else {

            }
        }
    });
    
    $(".sidebarSections").on("click", function () {
        $('#buttonToOpen').trigger('click');
        $('#addSection').modal('show'); 
    });


    $(".addProject, .addProjectPmanager").on("input change", function () {
        var projectName = $(".addProject").val();
        var projectManagerName = $(".addProjectPmanager").val();

        if (projectName.length > 0 && projectManagerName.length > 0) {
            $("#addProjectBtn").prop("disabled", false);
        }
        else {
            $("#addProjectBtn").prop("disabled", true);

        }
    });



    $(".addActivity").on("input", function () {
        var activityName = $(".addActivity").val();

        if (activityName.length > 0) {
            $(".addActivityBtn").prop("disabled", false);
        }
        else {
            $(".addActivityBtn").prop("disabled", true);

        }
    });


    $(".addSectionText").on("input", function () {
        var name = $(".addSectionText").val();

        if (name.length > 0) {
            $(".addSectionBtn").prop("disabled", false);
            $(".addSectionBtn").css("background-color", "#183B2B");
            $(".addSectionBtn").css("cursor", "pointer");
        }
        else {
            $(".addSectionBtn").prop("disabled", true);
            $(".addSectionBtn").css("background-color", "gray");
            $(".addSectionBtn").css("cursor", "not-allowed");

        }
    });

    $("#addLegendSection, #addLegendLow, #addLegendMed, #addLegendHigh").on("input", function () {

        var name = $("#addLegendSection").val();
        var la = $("#addLegendLow").val();
        var ma = $("#addLegendMed").val();
        var ha = $("#addLegendHigh").val();

        var low = parseInt(la);
        var med = parseInt(ma);
        var high = parseInt(ha);

        if (name.length > 0 && !isNaN(low) && !isNaN(med) && !isNaN(high)) {

            $(".addLEgendBtn").prop("disabled", false);
            $(".addLEgendBtn").css("background-color", "#183B2B");
            $(".addLEgendBtn").css("cursor", "pointer");

        }
        else {

            $(".addLEgendBtn").prop("disabled", true);
            $(".addLEgendBtn").css("background-color", "gray");
            $(".addLEgendBtn").css("cursor", "not-allowed");

        }

    });
    

    $(".updateProject").on("input", function () {
        var projectName = $(".updateProject").val();

        if (projectName.length > 0) {
            $("#addProjectBtn").prop("disabled", false);
        }
        else {
            $("#addProjectBtn").prop("disabled", true);

        }
    });


    $("#addNewEmailUser").on("focusout", function () {
        var email = $("#addNewEmailUser").val();

        if (email.length == 0) {

        }
        else {
            $("#addNewEmailUser").val(email + "@princeretail.com");
        }

    });

    



    $("#userSearch").on("input", function () {

        var resourceName = $("#userSearch").val();

        var bodyUser = $(".autoFillSearchUsersBody");

        if (resourceName.length > 0) {

            bodyUser.show();

            connection.invoke("GetUserAddDataSheet", resourceName)
                .catch(function (err) {
                    return console.error(err.toString());
                });
        }
        else {
            bodyUser.hide();
        }

    });



    $("#dataSheetSearch").on("input", function () {

        var resourceName = $("#dataSheetSearch").val();

        var bodyUser = $(".autoFillSearchUsersDataSheetBody");

        if (resourceName.length > 0) {

            bodyUser.show();

            connection.invoke("GetUserAddDataSheet", resourceName)
                .catch(function (err) {
                    return console.error(err.toString());
                });
        }
        else {
            bodyUser.hide();
        }

    });


    $("#dashboardSearch").on("input", function () {

        var resourceName = $("#dashboardSearch").val();

        var bodyUser = $(".autoFillSearchUsersDashboardBody");

        if (resourceName.length > 0) {

            bodyUser.show();

            connection.invoke("GetUserAddDataSheet", resourceName)
                .catch(function (err) {
                    return console.error(err.toString());
                });
            GetLegend(null);
        }
        else {
            bodyUser.hide();
            GetLegend(null);

        }

    });



    $("#updateDataSheetResourceName").on("input", function () {

        var resourceName = $("#updateDataSheetResourceName").val();

        var body = $(".autoFillSearchDataSheetBody");

        if (resourceName.length > 0) {

            body.show();

            connection.invoke("GetUserAddNewDataForSectionOnly", resourceName)
                .catch(function (err) {
                    return console.error(err.toString());
                });


            $.ajax({
                type: 'GET',
                url: "/Admin/AddDataSheetUserExistEnableButton",
                data: { name: resourceName },
                dataType: "json",
                success: function (result) {
 
                    if (result.result) {
                        body.hide();

                        $("#updateDataSheetButton").prop("disabled", false);

                        $("#updateDataSheetSection").val(result.section);
                        $("#updateDataSheetSectionDisplay").val(result.section);

                        $("#updateDataSheetResourceName").val(result.fullName);

                    }
                    else {

                        $("#updateDataSheetButton").prop("disabled", true);
                        $("#updateDataSheetSection").val("");
                        $("#updateDataSheetSectionDisplay").val("");
                    }
                },
                error: function (req, status, error) {
                    console.log(status);
                }
            });


        }
        else {
            body.hide();
            $("#updateDataSheetButton").prop("disabled", true);
            $("#updateDataSheetSection").val("");
            $("#updateDataSheetSectionDisplay").val("");
        }

    });


    
    $("#addDataSheetResourceName").on("input", function () {

        var resourceName = $("#addDataSheetResourceName").val();

        var body = $(".autoFillSearchDataSheetBody");

        if (resourceName.length > 0) {

            body.show();

            connection.invoke("GetUserAddNewDataForSectionOnly", resourceName)
                .catch(function (err) {
                    return console.error(err.toString());
                });


            $.ajax({
                type: 'GET',
                url: "/Admin/AddDataSheetUserExistEnableButton",
                data: { name: resourceName },
                dataType: "json",
                success: function (result) {

                    if (result.result) {
                        body.hide();
                        $("#addNewDataButton").prop("disabled", false);
                        $("#addDataSheetSection").val(result.section);
                        $("#addDataSheetSectionDisplay").val(result.section);
                        $("#addDataSheetResourceName").val(result.fullName);
                      
                    }
                    else {

                        $("#addNewDataButton").prop("disabled", true);
                        $("#addDataSheetSection").val("");
                        $("#addDataSheetSectionDisplay").val("");
                    }
                },
                error: function (req, status, error) {
                    console.log(status);
                }
            });


        }
        else {
            body.hide();
            $("#addNewDataButton").prop("disabled", true);
            $("#addDataSheetSection").val("");
            $("#addDataSheetSectionDisplay").val("");
        }

    });



    connection.on("GetUserAddDataSheetResult", function (users) {

        var container4 = $(".autoFillSearchUsersDataSheet");

        var container5 = $(".autoFillSearchUsersDashboard");

        if (users.length > 0) {




            container4.empty();
            container5.empty();

            users.forEach(function (user) {




                let usershtml5 = `
                     <div class="searchResult5" data-name="${user.fullName}">${user.fullName}</div>
                    
                `;


                container4.append(usershtml5);


                let usershtmDashboard = `
                     <div class="searchResultDashboard" data-name="${user.fullName}">${user.fullName}</div>
                `;

                container5.append(usershtmDashboard);



                $(".searchResultDashboard").on("click", function () {

                    var userName = $(this).data("name")

                    $("#dashboardSearch").val(userName);

                    $(".autoFillSearchUsersDashboardBody").hide();


                    var name = $("#dashboardSearch").val();

                    var years = $("#dashboardYear").val();

                    var year = parseInt(years);

                    var section = $("#dashboardSectionDropDown").val();

                    var sectionId = parseInt(section);

                    connection.invoke("GetUsersMonthlyStatisticsSort", sectionId, name, year)
                        .catch(function (err) {
                            return console.error(err.toString());
                        });

                });






                $(".searchResult5").on("click", function () {

                    var userName = $(this).data("name")

                    $("#dataSheetSearch").val(userName);

                    $(".autoFillSearchUsersDataSheetBody").hide();


                    var name = $("#dataSheetSearch").val();

                    var years = $("#dataSheetYear").val();

                    var year = parseInt(years);

                    var section = $("#dataSheetSectionDropDown").val()

                    var sectionId = parseInt(section);

                    if (section === "SECTION") {

                        connection.invoke("GetAllDataSheetSort", null, name, year)
                            .catch(function (err) {
                                return console.error(err.toString());
                            });
                    }
                    else {
                        connection.invoke("GetAllDataSheetSort", sectionId, name, year)
                            .catch(function (err) {
                                return console.error(err.toString());
                            });
                    }



                });



            });


        }
        else {

            container4.empty();
            container5.empty();

            var htmls = `
            <div class="searchResult">No User Found!</div>
                `;

            container4.append(htmls);
            container5.append(htmls);
        }

    });

    connection.on("GetUserAddNewDataForSectionOnlyResult", function (users) {

        var container = $(".autoFillSearchDataSheet");


        var container2 = $(".autoFillSearchUsers");


        var container3 = $(".autoFillSearchDataSheetUpdate");


        if (users.length > 0) {

            container.empty();

            container2.empty();

            container3.empty();

            users.forEach(function (user) {

                let usershtml = `
                     <div class="searchResult" data-name="${user.fullName}">${user.fullName}</div>
                `;
                container.append(usershtml);


                let usershtml1 = `
                     <div class="searchResult1" data-name="${user.fullName}">${user.fullName}</div>
                `;
                container2.append(usershtml1);


                let usershtml2 = `
                     <div class="searchResult2" data-name="${user.fullName}">${user.fullName}</div>
                `;
                container3.append(usershtml2);


                $(".searchResult").on("click", function () {



                    var userName = $(this).data("name");

                    $("#addDataSheetResourceName").val(userName);

                    $(".autoFillSearchDataSheetBody").hide();

                    var resourceName = $("#addDataSheetResourceName").val();

                    $.ajax({
                        type: 'GET',
                        url: "/Admin/AddDataSheetUserExist",
                        data: { name: resourceName },
                        dataType: "json",
                        success: function (result) {
                            if (result == "No") {

                                alert("This User Doesn't have a Section! Please put a Section of this User First!");
                                $("#addDataSheetSection").val("");
                                $("#addDataSheetSectionDisplay").val("");
                            }
                            else {

                                $("#addNewDataButton").prop("disabled", false);

                                $("#addDataSheetSection").val(result);
                                $("#addDataSheetSectionDisplay").val(result);

                            }
                        },
                        error: function (req, status, error) {
                            console.log(status);
                        }
                    });


                });

                $(".searchResult2").on("click", function () {

                    var userName = $(this).data("name")

                    $("#updateDataSheetResourceName").val(userName);

                    $(".autoFillSearchDataSheetBody").hide();


                    var resourceName = $("#updateDataSheetResourceName").val();



                    $.ajax({
                        type: 'GET',
                        url: "/Admin/AddDataSheetUserExist",
                        data: { name: resourceName },
                        dataType: "json",
                        success: function (result) {

                            if (result == "No") {

                                alert("This User Doesn't have a Section! Please put a Section of this User First!");
                                $("#updateDataSheetSection").val("");
                                $("#updateDataSheetSectionDisplay").val("");
                            }
                            else {

                                $("#updateDataSheetButton").prop("disabled", false);

                                $("#updateDataSheetSection").val(result);
                                $("#updateDataSheetSectionDisplay").val(result);
                            }
                        },
                        error: function (req, status, error) {
                            console.log(status);
                        }
                    });
                });

                $(".searchResult1").on("click", function () {

                    var userName = $(this).data("name")

                    $("#userSearch").val(userName);

                    $(".autoFillSearchUsersBody").hide();


                    var search = $("#userSearch").val();

                    connection.invoke("GetUserSearch", search)
                        .catch(function (err) {
                            return console.error(err.toString());
                        });

                });

            });


        }
        else {
            container.empty();
            container2.empty();
            container3.empty();

            var htmls = `
            <div class="searchResult">No User Found!</div>
                `;

            container.append(htmls);
            container2.append(htmls);
            container3.append(htmls);
        }

    });

    $("#userSearch").on("input", function () {

        var search = $("#userSearch").val();


        if (search.length > 0 ) {
  
            connection.invoke("GetUserSearch", search)
                .catch(function (err) {
                    return console.error(err.toString());
                });

            connection.invoke("GetUserAddNewDataForSectionOnly", search)
                .catch(function (err) {
                    return console.error(err.toString());
                });
        }
        else {
            GetAllUsers();
            $("#userNames").empty();
            $("#userNames").children("option").css("display", "none");
        }

    });


    connection.on("ReceiveAllUsers", function (users) {
        var container = $(".usersBodyDiv2Inner");

        var usersDataList = $(".usersDataList");


        if (users.length > 0) {
            container.empty();

            usersDataList.empty();

            users.forEach(function (user) {


                let dashboard = `<option value="${user.fullName}"/>`;
 

                let usersDataList1 = `<option value="${user.fullName}"/>`;

                usersDataList.append(usersDataList1);

                let usershtml = `<div class="usersBodyDiv2InnerCards">
                    <div class="usersBodyDiv2InnerCardsDiv1">
                      <img src="/profile/${user.profileString || "default.jpg"}" class="usersProfilePic" />
                    </div>
                    <div class="usersBodyDiv2InnerCardsDiv2">
                      <div class="usersBodyDiv2InnerCardsDiv2Inner1">
                        <h5>${user.fullName}</h5>
                        <h6>${user.position}</h6>
                      </div>
                      <div class="usersBodyDiv2InnerCardsDiv2Inner2">
                        <button id="usersModal" class="user-modal-btn" data-name="${user.fullName}" data-position="${user.position}" data-email="${user.email}" data-number="${user.phoneNumber}" data-profilestring="${user.profileString}" data-section="${user.section?.sectionName || ' No Section Yet'}" data-bs-toggle="modal" data-bs-target="#usersProfileModal"><i class="fa fa-user-circle" aria-hidden="true"></i></button>
                        <button class="delete-user-btn" data-name="${user.fullName}" data-id="${user.id}" data-bs-toggle="modal" data-bs-target="#deleteUserModal"><i class="fa fa-trash" aria-hidden="true"></i></button>
                      </div>
                    </div>
                  </div>`;

                container.append(usershtml);

                $(".user-modal-btn").on("click", function () {

                    $(".usersName").empty();
                    $(".usersPosition").empty();
                    $(".usersEmail").empty();


                    $(".usersSection").empty();

                    $(".usersProfilePicModal").empty();

                    $(".usersName").text($(this).data("name"));
                    $(".usersPosition").text($(this).data("position") || "No Position yet");
                    $(".usersEmail").text($(this).data("email") || "No Email yet");
       
                    $(".usersSection").text($(this).data("section"));

                    var profileString = $(this).data("profilestring");
                    if (profileString) {
                        $(".usersProfilePicModal").attr("src", `/profile/${profileString}`);
                    } else {
                        $(".usersProfilePicModal").attr("src", "/profile/default.jpg");
                    }

                });

                $(".delete-user-btn").on("click", function () {

                    $("#userNameToDelete").empty();


                    var userName = $(this).data("name");
                    var userId = $(this).data("id");


                    $("#userNameToDelete").html("<span style='font-weight:bold;'>" + userName + " ?</span>");


                    $(".userIdToDelete").val(userId);
                });



            });
        }
        else {
            var htmls = `
            <div style="width: 60%; height: 55%; position: absolute;display:flex; justify-content:center; align-items:center;">
                <h1 style="color:#425833;">No Other Users</h1>
            </div>
        `;

            container.append(htmls);
        }

    });


    connection.on("ReceiveAllUsersSearch", function (searchedUsers) {

            var container = $(".usersBodyDiv2Inner");

            var dashboardDataList = $(".dashboardDataList");

            var usersDataList = $(".usersDataList");


        if (searchedUsers.length > 0) {


                container.empty();

                dashboardDataList.empty();

                usersDataList.empty();

                searchedUsers.forEach(function (user) {

                    let dashboard = `<option value="${user.fullName}"/>`;
                    dashboardDataList.append(dashboard);

                    let usersDataList1 = `<option value="${user.fullName}"/>`;

                    usersDataList.append(usersDataList1);

                    let usershtml = `<div class="usersBodyDiv2InnerCards">
                    <div class="usersBodyDiv2InnerCardsDiv1">
                      <img src="/profile/${user.profileString || "default.jpg"}" class="usersProfilePic" />
                    </div>
                    <div class="usersBodyDiv2InnerCardsDiv2">
                      <div class="usersBodyDiv2InnerCardsDiv2Inner1">
                        <h5>${user.fullName}</h5>
                         <h6>${user.position}</h6>
                      </div>
                      <div class="usersBodyDiv2InnerCardsDiv2Inner2">
                        <button id="usersModal" class="user-modal-btn" data-name="${user.fullName}" data-position="${user.position}" data-email="${user.email}" data-number="${user.phoneNumber}" data-profilestring="${user.profileString}" data-section="${user.section?.sectionName || ' No Section Yet'}" data-bs-toggle="modal" data-bs-target="#usersProfileModal"><i class="fa fa-user-circle" aria-hidden="true"></i></button>
                        <button class="delete-user-btn" data-name="${user.fullName}" data-id="${user.id}" data-bs-toggle="modal" data-bs-target="#deleteUserModal"><i class="fa fa-trash" aria-hidden="true"></i></button>
                      </div>
                    </div>
                  </div>`;

                    container.append(usershtml);

                    $(".user-modal-btn").on("click", function () {

                        $(".usersName").empty();
                        $(".usersPosition").empty();
                        $(".usersEmail").empty();
                        $(".usersNumber").empty();

                        $(".usersSection").empty();

                        $(".usersProfilePicModal").empty();

                        $(".usersName").text($(this).data("name"));
                        $(".usersPosition").text($(this).data("position") || "No Position yet");
                        $(".usersEmail").text($(this).data("email") || "No Email yet");
                        $(".usersNumber").text($(this).data("number") || "No Number Yet");
                        $(".usersSection").text($(this).data("section"));

                        var profileString = $(this).data("profilestring");
                        if (profileString) {
                            $(".usersProfilePicModal").attr("src", `/profile/${profileString}`);
                        } else {
                            $(".usersProfilePicModal").attr("src", "/profile/default.jpg");
                        }

                    });

                    $(".delete-user-btn").on("click", function () {

                        $("#userNameToDelete").empty();


                        var userName = $(this).data("name");
                        var userId = $(this).data("id");


                        $("#userNameToDelete").html("<span style='font-weight:bold;'>" + userName + " ?</span>");


                        $(".userIdToDelete").val(userId);
                    });



                });
            }
            else {
                container.empty();

                let html = `
                    <div style="height:100%; width:100%;display:flex;justify-content:center;align-items:center;">
                        <h1 style="color:#425833;">No Users Found!</h1>
                    </div>
                `;

                container.append(html);

            }

        });


    connection.on("SectionsDropdown", function (sections) {

        var userEditSection = $("#userEditSection");

        var addDataSheetSection = $("#addDataSheetSection");

        var dashboard = $("#dashboardSectionDropDown");

        var dashboardLegend = $(".sectionforLegend");

        var userSection = $("#userSection");

        var sectionSettingsBody = $(".sectionSettingsBody");

        var dataSheetSectionDropdown = $("#dataSheetSectionDropDown");


        if (sections.length > 0) {

            dashboard.empty();
            dataSheetSectionDropdown.empty();

            dashboardLegend.empty();

            userSection.empty();

            $("#addNewUserSectionDrop").empty();

            let htmluser11 = `<option disabled selected>Section</option>`;
            $("#addNewUserSectionDrop").append(htmluser11);

            sectionSettingsBody.empty();


            let htmldash1 = `<option>SECTION</option>`;
            dashboard.append(htmldash1);

            dataSheetSectionDropdown.append(htmldash1);

            let htmldash2 = `<option value="0">SECTION</option>`;
            dashboardLegend.append(htmldash2);

            userSection.append(htmldash2);


            sections.forEach(function (section) {

                let html = `<option value="${section.sectionId}">${section.sectionName}</option>`;

                let htmlForDataSheet = `<option value="${section.sectionName}">${section.sectionName}</option>`;

                dashboard.append(html);

                dataSheetSectionDropdown.append(html);

                dashboardLegend.append(html);
                userEditSection.append(html);
                userSection.append(html);

                addDataSheetSection.append(htmlForDataSheet);


                $("#addNewUserSectionDrop").append(html);

                let addSectionHtml = `     <div class="sectionSettingsBodyDiv1">
                    <input type="text" class="form-control addNewUserInput" value="${section.sectionName}" autocomplete="off" disabled/>
                    <i class="fa fa-pencil edit-section" aria-hidden="true" data-name="${section.sectionName}" data-id="${section.sectionId}" data-bs-toggle="modal" data-bs-target="#editSectionModal"></i>
                    <i class="fa fa-trash delete-section" aria-hidden="true" data-name="${section.sectionName}" data-id="${section.sectionId}" data-bs-toggle="modal" data-bs-target="#deleteSectionModal"></i>
                </div>`;

                sectionSettingsBody.append(addSectionHtml);


                $(".delete-section").on("click", function () {
                    $("#sectionNameToDelete").empty();
                    var userName = $(this).data("name");
                    var sectionId = $(this).data("id");
                    $("#sectionNameToDelete").html("<span style='font-weight:bold;'>" + userName + " ?</span>");
                    $(".sectionIdToDelete").val(sectionId);
                });

                $(".edit-section").on("click", function () {
                    $(".sectionNameToEdit").empty();
                    $(".sectionIdToEdit").empty();
                    var userName = $(this).data("name");
                    var sectionId = $(this).data("id");
                    $(".sectionNameToEdit").val(userName);
                    $(".sectionIdToEdit").val(sectionId);
                });




            });

        }
        else {

        }

    });
    

    connection.on("LegendSectionsDropdown", function (sections) {
        var addLegendSection = $("#addLegendSection");
        addLegendSection.empty();

        let html = `<option disabled="" selected="">SELECT SECTION</option>`;

        addLegendSection.append(html);
        sections.forEach(function (sectionArray) {
            let section = sectionArray[0];
            //console.log(section);

            let htmlForLegend = `<option value="${section.sectionId}" ${section.status}>${section.sectionName}</option>`;

            addLegendSection.append(htmlForLegend);
        });
    });


    connection.on("AllUsersCount", function (users) {
        $("#totalUsers").text(users);
    });




    //connection.on("AllDataSheet", function (datas) {

    //    var container = $(".tbodyDataSheet");

    //    if (datas.length > 0) {

    //        container.empty();

    //        datas.forEach(function (data) {

    //            var startDate = new Date(data.startDate);
    //            var endDate = new Date(data.endDate);

    //            var formattedStartDate = startDate.toLocaleDateString("en-US", {
    //                month: "long",
    //                day: "numeric",
    //                year: "numeric"
    //            });

    //            var formattedEndDate = endDate.toLocaleDateString("en-US", {
    //                month: "long",
    //                day: "numeric",
    //                year: "numeric"
    //            });

    //            let shtml = `            <tr>
    //                    <td>${data.section.sectionName}</td>
    //                    <td>${data.appUser.fullName}</td>
    //                    <td>${data.project.projectName}</td>
    //                    <td>${data.activity.activityName}</td>
    //                    <td>${data.businessOrIt.businessOrItName}</td>
    //                    <td>${formattedStartDate}</td>
    //                    <td>${formattedEndDate}</td>
    //                    <td>${data.hoursPerDay}</td>
    //                    <td>${data.hoursPerMonth}</td>
    //                    <td>
    //                        <i class="fa fa-pencil dataSheetUpdate" aria-hidden="true"
    //                        data-section="${data.section.sectionName}" data-fullname="${data.appUser.fullName}" 
    //                        data-project="${data.project.projectName}" data-activity="${data.activity.activityName}" 
    //                        data-business="${data.businessOrIt.businessOrItName}" data-start="${data.startDate}" 
    //                        data-endd="${data.endDate}" data-day="${data.hoursPerDay}" data-month="${data.hoursPerMonth}" 
    //                        data-id="${data.dataSheetBusId}" data-bs-toggle="modal" data-bs-target="#updateDataSheetModal"></i>

    //                        <i class="fa fa-trash dataSheetDelete" aria-hidden="true" data-id="${data.dataSheetBusId}"
    //                        data-bs-toggle="modal" data-bs-target="#deleteDataSheetModal"></i>
    //                    </td>
    //                </tr>`;

    //            container.append(shtml);


    //            $(".dataSheetUpdate").on("click", function () {

    //                var dataSheetId = $(this).data("id");
    //                var sectionName = $(this).data("section");
    //                var fullName = $(this).data("fullname");
    //                var projectName = $(this).data("project");
    //                var activityName = $(this).data("activity");
    //                var businessOrIt = $(this).data("business");
    //                var startDate = $(this).data("start");
    //                var endDate = $(this).data("endd");
    //                var hoursPerDay = parseFloat($(this).data("day"));
    //                var hoursPerMonth = $(this).data("month");

    
    //                $("#updateDataSheetId").val(dataSheetId);
    //                //$("#updateDataSheetSectionValue").empty();
    //                //let htm = `
    //                //    ${sectionName}
    //                //`;

    //                //$("#updateDataSheetSectionValue").append(htm);
    //                $("#updateDataSheetSection").val(sectionName);
    //                $("#updateDataSheetSectionDisplay").val(sectionName);
    //                $("#updateDataSheetResourceName").val(fullName);
    //                $("#updateDataSheetProjectName").val(projectName);
    //                $("#updateDataSheetActivityName").val(activityName);
    //                $("#updateDataSheetBusinessOrIT").val(businessOrIt);
    //                $("#updateDataSheetStartDate").val(startDate);

    //                $("#updateDataSheetEndDate").val(endDate);
                    
    //                $("#updateDataSheetWorkingHours").val(hoursPerDay);
    //                $("#updateDataSheetTotalHoursDisplay").val(hoursPerMonth);
    //            });



    //            $(".dataSheetDelete").on("click", function () {

    //                var dataSheetId = $(this).data("id");

    //                $("#dataSheetIdDelete").val(dataSheetId);
                    
    //            });






    //        });
    //    }
    //    else {
    //        container.empty();

    //        let htmls = `<div style="width: 80%; height: 60%; position: absolute;display:flex; justify-content:center; align-items:center;">
    //            <h1 style="color:#425833;">No Data</h1>
    //        </div>`;

    //        container.append(htmls);

    //    }
    //});



    connection.on("GetAllDataSheetSort", function (datas) {

        var container = $(".tbodyDataSheet");


        if (datas.data.length > 0) {

            container.empty();

            if (datas.role == "Manager") {
                datas.data.forEach(function (data) {

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


                    if (new Date(data.startDate).getFullYear() < currentYear && new Date(data.endDate).getFullYear() < currentYear) {
                        let shtml = `    
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
                        <td>

                        </td>
                    </tr>`;

                        container.append(shtml);
                    }
                    else {

                        let shtml = `    
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
                        <td>
                            <i class="fa fa-pencil dataSheetUpdate" aria-hidden="true"
                            data-section="${data.section.sectionName}" data-fullname="${data.appUser.fullName}" 
                            data-project="${data.project.projectName}" data-activity="${data.activity.activityName}" 
                            data-business="${data.businessOrIt.businessOrItName}" data-start="${data.startDate}" 
                            data-endd="${data.endDate}" data-day="${data.hoursPerDay}" data-month="${data.hoursPerMonth}" 
                            data-id="${data.dataSheetBusId}" data-bs-toggle="modal" data-bs-target="#updateDataSheetModal"></i>

                            <i class="fa fa-trash dataSheetDelete" aria-hidden="true" data-id="${data.dataSheetBusId}"
                            data-bs-toggle="modal" data-bs-target="#deleteDataSheetModal"></i>
                        </td>
                    </tr>`;

                        container.append(shtml);


                        $(".dataSheetUpdate").on("click", function () {

                            var dataSheetId = $(this).data("id");
                            var sectionName = $(this).data("section");
                            var fullName = $(this).data("fullname");
                            var projectName = $(this).data("project");
                            var activityName = $(this).data("activity");
                            var businessOrIt = $(this).data("business");
                            var startDate = $(this).data("start");
                            var endDate = $(this).data("endd");
                            var hoursPerDay = parseFloat($(this).data("day"));
                            var hoursPerMonth = $(this).data("month");


                            $("#updateDataSheetId").val(dataSheetId);
                            $("#updateDataSheetSection").val(sectionName);
                            $("#updateDataSheetSectionDisplay").val(sectionName);
                            $("#updateDataSheetResourceName").val(fullName);
                            $("#updateDataSheetProjectName").val(projectName);
                            $("#updateDataSheetActivityName").val(activityName);
                            $("#updateDataSheetBusinessOrIT").val(businessOrIt);
                            $("#updateDataSheetStartDate").val(startDate);

                            $("#updateDataSheetEndDate").val(endDate);

                            $("#updateDataSheetWorkingHours").val(hoursPerDay);
                            $("#updateDataSheetTotalHoursDisplay").val(hoursPerMonth);
                        });



                        $(".dataSheetDelete").on("click", function () {

                            var dataSheetId = $(this).data("id");

                            $("#dataSheetIdDelete").val(dataSheetId);

                        });
                    }



                });
            }
            else if (datas.role == "Project_Manager" || datas.role == "Admin") {
                datas.data.forEach(function (data) {

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


                    if (new Date(data.startDate).getFullYear() < currentYear && new Date(data.endDate).getFullYear() < currentYear) {
                        let shtml = `    
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
                        <td>

                        </td>
                    </tr>`;

                        container.append(shtml);
                    }
                    else {

                        let shtml = `    
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
                        <td>
                            <i class="fa fa-times dataSheetUpdate" aria-hidden="true"
                            data-section="${data.section.sectionName}" data-fullname="${data.appUser.fullName}" 
                            data-project="${data.project.projectName}" data-activity="${data.activity.activityName}" 
                            data-business="${data.businessOrIt.businessOrItName}" data-start="${data.startDate}" 
                            data-endd="${data.endDate}" data-day="${data.hoursPerDay}" data-month="${data.hoursPerMonth}" 
                            data-id="${data.dataSheetBusId}" data-bs-toggle="modal" data-bs-target="#updateDataSheetModal" style="pointer-events: none;"></i>

                            <i class="fa fa-times dataSheetDelete" aria-hidden="true" data-id="${data.dataSheetBusId}"
                            data-bs-toggle="modal" data-bs-target="#deleteDataSheetModal" style="pointer-events: none;"></i>
                        </td>
                    </tr>`;

                        container.append(shtml);

                    }

                });

            }

        }
        else {
            container.empty();

            let htmls = `<div style="width: 80%; height: 60%; position: absolute;display:flex; justify-content:center; align-items:center;">
                <h1 style="color:#425833;"><span style="font-weight:bold;"> </span>No Data!</h1>
            </div>`;

            container.append(htmls);

        }
    });



    function isHoliday(date) {
        const dateStr = date.toISOString().slice(0, 10);
        return holidays.some(holiday => holiday.fixedDate === dateStr);
    }
    function getHolidayName(date) {
        const dateStr = date.toISOString().slice(0, 10);
        const foundHoliday = holidays.find(h => h.fixedDate === dateStr);
        return foundHoliday ? foundHoliday.fixedName : null;
    }
    function isWeekday(date) {
        const day = date.getDay();
        return day > 0 && day < 6;
    }

    $("#hoursperday").on("input", function () {
        const startDate = new Date(document.getElementById('startDate').value);
        const endDate = new Date(document.getElementById('endDate').value);
        const hoursPerDay = parseFloat(document.getElementById('hoursperday').value);
        let totalWorkHours = 0;
        let currentDate = new Date(startDate);
        let holidayNames = [];

        while (currentDate <= endDate) {
            if (isWeekday(currentDate) && !isHoliday(currentDate)) {
                totalWorkHours += hoursPerDay;
            } else if (isHoliday(currentDate)) {
                const holidayName = getHolidayName(currentDate);
                if (holidayName) {
                    holidayNames.push(holidayName);
                }
            }
            currentDate.setDate(currentDate.getDate() + 1);
        }
        $("#TotalHoursPerMonth").val(totalWorkHours);
        $("#TotalHoursPerMonthDisplay").val(totalWorkHours);
    });


    $("#updateDataSheetWorkingHours").on("input", function () {

        const startDate = new Date(document.getElementById('updateDataSheetStartDate').value);
        const endDate = new Date(document.getElementById('updateDataSheetEndDate').value);
        const hoursPerDay = parseFloat(document.getElementById('updateDataSheetWorkingHours').value);

        let totalWorkHours = 0;
        let currentDate = new Date(startDate);
        let holidayNames = [];

        while (currentDate <= endDate) {
            if (isWeekday(currentDate) && !isHoliday(currentDate)) {
                totalWorkHours += hoursPerDay;
            } else if (isHoliday(currentDate)) {
                const holidayName = getHolidayName(currentDate);
                if (holidayName) {
                    holidayNames.push(holidayName);
                }
            }
            currentDate.setDate(currentDate.getDate() + 1);
        }
        $("#updateDataSheetTotalHours").val(totalWorkHours);

        $("#updateDataSheetTotalHoursDisplay").val(totalWorkHours);
    });





    function isHoliday(date) {
        const dateStr = date.toISOString().slice(0, 10);
        return holidays.some(holiday => holiday.fixedDate === dateStr);
    }


    function isWeekday(date) {
        const day = date.getDay();
        return day > 0 && day < 6;
    }

    function isHoliday(date) {
        const dateStr = date.toISOString().slice(0, 10);
        return holidays.some(holiday => holiday.fixedDate === dateStr);
    }
    function getHolidayName(date) {
        const dateStr = date.toISOString().slice(0, 10);
        const foundHoliday = holidays.find(h => h.fixedDate === dateStr);
        return foundHoliday ? foundHoliday.fixedName : null;
    }
    function isWeekday(date) {
        const day = date.getDay();
        return day > 0 && day < 6;
    }


    //function calculateMonthlyWorkHours(startDate, endDate, hoursPerDay) {
    //    let totalWorkHours = {
    //        january: 0, february: 0, march: 0, april: 0, may: 0, june: 0,
    //        july: 0, august: 0, september: 0, october: 0, november: 0, december: 0
    //    };

    //    let totalHours = 0;

    //    let currentDate = new Date(startDate);

    //    while (currentDate <= endDate) {
    //        if (isWeekday(currentDate) && !isHoliday(currentDate)) {
    //            const month = currentDate.getMonth();
    //            totalWorkHours[Object.keys(totalWorkHours)[month]] += hoursPerDay;
    //            totalHours += hoursPerDay;

    //        }
    //        currentDate.setDate(currentDate.getDate() + 1);
    //    }

    //    return {
    //        monthlyWorkHours: totalWorkHours,
    //        totalWorkHours: totalHours
    //    };
    //}

    $("#hoursperday, #startDate, #endDate").on("input", function () {

        const startDate = new Date(document.getElementById('startDate').value);
        const endDate = new Date(document.getElementById('endDate').value);
        const hoursPerDay = parseFloat(document.getElementById('hoursperday').value);


        if (!isNaN(startDate.getTime())) {
            if (startDate.getFullYear() !== new Date().getFullYear()) {
                alert("The Start Date Year Must Be This Year!");
                $("#startDate").val("");
            }
        }

        if (!isNaN(startDate.getTime()) && !isNaN(endDate.getTime())) {
            if (startDate.getFullYear() !== endDate.getFullYear()) {
                alert("The Start Date Year and End Date Year Must Be the same!");
                $("#endDate").val("");
            }
        }

        let totalWorkHours = 0;
        let currentDate = new Date(startDate);
        let holidayNames = [];

        while (currentDate <= endDate) {
            if (isWeekday(currentDate) && !isHoliday(currentDate)) {
                totalWorkHours += hoursPerDay;
            } else if (isHoliday(currentDate)) {
                const holidayName = getHolidayName(currentDate);
                if (holidayName) {
                    holidayNames.push(holidayName);
                }
            }
            currentDate.setDate(currentDate.getDate() + 1);
        }

        $("#TotalHoursPerMonth").val(totalWorkHours);
        $("#TotalHoursPerMonthDisplay").val(totalWorkHours);

    });


    $("#updateDataSheetWorkingHours, #updateDataSheetStartDate, #updateDataSheetEndDate").on("input", function () {

        const startDate = new Date(document.getElementById('updateDataSheetStartDate').value);
        const endDate = new Date(document.getElementById('updateDataSheetEndDate').value);
        const hoursPerDay = parseFloat(document.getElementById('updateDataSheetWorkingHours').value);


        if (!isNaN(startDate.getTime()) && !isNaN(endDate.getTime())) {

            if (startDate.getFullYear() !== endDate.getFullYear()) {
                alert("The Start Date Year and End Date Year is not the same !");

                $("#updateDataSheetEndDate").val("");

            }
            else {

            }
        }



        let totalWorkHours = 0;
        let currentDate = new Date(startDate);
        let holidayNames = [];

        while (currentDate <= endDate) {
            if (isWeekday(currentDate) && !isHoliday(currentDate)) {
                totalWorkHours += hoursPerDay;
            } else if (isHoliday(currentDate)) {
                const holidayName = getHolidayName(currentDate);
                if (holidayName) {
                    holidayNames.push(holidayName);
                }
            }
            currentDate.setDate(currentDate.getDate() + 1);
        }

        $("#updateDataSheetTotalHours").val(totalWorkHours);
        $("#updateDataSheetTotalHoursDisplay").val(totalWorkHours);

    });


    connection.on("UsersMonthlyStatisticsSort", function (users) {

        var container = $(".tbodyDashboard");

        var search = $("#dashboardSearch").val();

        if (users.length > 0) {

            container.empty();

            users.forEach(function (user) {

                let html = `
                     <tr>
                        <td>${user.sectionName}</td>
                        <td>${user.appUserName}</td>
                        <td  style="${getBackgroundColor(user.january, user.low, user.med, user.max)};">${user.january}</td>
                        <td  style="${getBackgroundColor(user.february, user.low, user.med, user.max)};">${user.february} </td>
                        <td  style="${getBackgroundColor(user.march, user.low, user.med, user.max)};">${user.march} </td>
                        <td  style="${getBackgroundColor(user.april, user.low, user.med, user.max)};">${user.april} </td>
                        <td  style="${getBackgroundColor(user.may, user.low, user.med, user.max)};">${user.may} </td>
                        <td  style="${getBackgroundColor(user.june, user.low, user.med, user.max)};">${user.june} </td>
                        <td  style="${getBackgroundColor(user.july, user.low, user.med, user.max)};">${user.july} </td>
                        <td  style="${getBackgroundColor(user.august, user.low, user.med, user.max)};">${user.august} </td>
                        <td  style="${getBackgroundColor(user.september, user.low, user.med, user.max)};">${user.september} </td>
                        <td  style="${getBackgroundColor(user.october, user.low, user.med, user.max)};">${user.october} </td>
                        <td style="${getBackgroundColor(user.november, user.low, user.med, user.max)};">${user.november} </td>
                        <td  style="${getBackgroundColor(user.december, user.low, user.med, user.max)};">${user.december} </td>
                        <td >${user.totalHours} </td>
                    </tr>
                `;

                container.append(html);

            });
        }
        else {

            container.empty();

            let htmls = `<div style="width: 80%; height: 60%; position: absolute;display:flex; justify-content:center; align-items:center;">
                <h1 style="color:#425833;"><span style="font-weight:bold;">${search} </span>Not Found!</h1>
            </div>`;

            container.append(htmls);
        }

    });


    function getBackgroundColor(value, low, med, max) {

        if (low != 0 && med != 0 && max != 0) {
            if (value == 0) {
                return "background-color: white; color:black";
            }
            else if (value <= low) {
                return "background-color: #DED710; color:black";
            } else if (value <= med) {
                return "background-color: #C3942C; color:white";
            } else if (value <= max) {
                return "background-color: #A9C393; color:white";
            } else {
                return "background-color: #E24848; color:white";
            }
        }
        else {
            return "background-color: white; color:red;";
        }

    }


    connection.on("AllowedHours", function (allowed) {
       
        var container = $(".allworkingHoursModalBodyDiv1");

        var allowedHoursDropdown = $("#hoursperday");

        var allowedHoursDropdown2 = $("#updateDataSheetWorkingHours");
        allowedHoursDropdown.empty();
        allowedHoursDropdown2.empty();
        container.empty();
        allowed.forEach(function (allowedHours) {


            let workingHoursSettings = `<div class="sectionSettingsBodyDiv1">
                    <input type="text" class="form-control addNewUserInput" value="${allowedHours.number}" autocomplete="off" disabled style="text-align:center;"/>
                    <i class="fa fa-pencil edit-allowedHours" aria-hidden="true" data-name="${allowedHours.number}" data-id="${allowedHours.allowedHoursId}" data-bs-toggle="modal" data-bs-target="#editworkingHoursModal"></i>
                    <i class="fa fa-trash delete-allowedHours" aria-hidden="true" data-name="${allowedHours.number}" data-id="${allowedHours.allowedHoursId}" data-bs-toggle="modal" data-bs-target="#deleteWrokingHoursModal"></i>
                </div>`;

            container.append(workingHoursSettings);

            $(".delete-allowedHours").on("click", function () {
                $("#allowedHoursNameToDelete").empty();
                var userName = $(this).data("name");
                var allowedId = $(this).data("id");
                $("#allowedHoursNameToDelete").html("<span style='font-weight:bold;'>" + userName + " ?</span>");
                $(".allowedHoursIdToDelete").val(allowedId);
            });

            $(".edit-allowedHours").on("click", function () {
                $(".allowedHoursNameToEdit").empty();
                $(".allowedHoursIdToEdit").empty();
                var userName = $(this).data("name");
                var allowedId = $(this).data("id");
                $(".allowedHoursNameToEdit").val(userName);
                $(".allowedHoursIdToEdit").val(allowedId);
            });


            let html = `
                 <option value="${allowedHours.number}">${allowedHours.number}</option>
            `;

            allowedHoursDropdown.append(html);

            allowedHoursDropdown2.append(html);


        });

    });








    function GetAllUsers() {
        connection.invoke("GetAllUsers")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }

    function GetSectionsDropdown() {
        connection.invoke("GetSections")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }

    function GetSectionsLegends() {
        connection.invoke("GetLegendSections")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }


    function GetAllUsersCount() {
        connection.invoke("GetAllUsersCount")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }


    function GetUsersMonthlyStatistics() {

        connection.invoke("GetUsersMonthlyStatistics")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }
    connection.on("Legends", function (legend) {


        if (legend == null) {
            $("#legendLow").text("");
            $("#legendMed").text("");
            $("#legendMax").text("");
        }
        else {

            $("#legendLow").text(legend.low);
            $("#legendMed").text(legend.med);
            $("#legendMax").text(legend.max);
        }

    });




    $(".sectionforLegend").on("input", function () {

        var section = $(".sectionforLegend").val();

        var sec = parseInt(section);

        if (sec == 0) {
            $("#legendLow").text("");
            $("#legendMed").text("");
            $("#legendMax").text("");
        }
        else {
            GetLegend(sec);
        }

    });







    connection.on("GetAllLegends", function (legend) {

        var container = $(".legendSettingsBody");
        
        if (legend.length > 0) {

            legend.forEach(function (legends) {
        
                sectionLegends.push(legends);

                let html = `
                    <div class="legendSettingsBodyDiv1">
                        <input type="text" value="${legends.section.sectionName}" class="form-control addNewUserInput"  autocomplete="off" disabled style="width:40%;"/>
                        <input type="text" value="${legends.low}" class="form-control addNewUserInput" autocomplete="off" disabled style="width:13%;" />
                        <input type="text" value="${legends.med}" class="form-control addNewUserInput" autocomplete="off" disabled style="width:13%;" />
                        <input type="text" value="${legends.max}" class="form-control addNewUserInput" autocomplete="off" disabled style="width:13%;" />
                        <i class="fa fa-pencil edit-legend" aria-hidden="true" data-id="${legends.legendId}" data-sectionid="${legends.sectionId}"
                        data-name="${legends.section.sectionName}" data-low="${legends.low}" data-med="${legends.med}" data-max="${legends.max}" data-bs-toggle="modal" data-bs-target="#editLegendModal"></i>
                        <i class="fa fa-trash delete-legend" aria-hidden="true" data-id="${legends.legendId}" data-name="${legends.section.sectionName}" data-bs-toggle="modal" data-bs-target="#deleteLegendModal"></i>
                    </div>
                `;

                container.append(html);

                $(".edit-legend").on("click", function () {

                    var legendId = $(this).data("id");
                    var sectionId = $(this).data("sectionid");
                    var sectionName = $(this).data("name");
                    var low = $(this).data("low");
                    var med = $(this).data("med");
                    var max = $(this).data("max");
               


                    $("#legendId").val(legendId);
                    $("#sectionId").val(sectionId);
                    $("#sectionName").val(sectionName);
                    $("#legendLow").val(low);
                    $("#legendMed").val(med);
                    $("#legendMax").val(max);

                });

                $(".delete-legend").on("click", function () {

                    var legendId = $(this).data("id");
                    var sectionName = $(this).data("name");

                    $("#legendIdToDelete").val(legendId);
                    $("#legendNameToDelete").text(sectionName+"?");

                });



            });

        }
        else {

        }

    });



    function GetLegend(legendId) {


        connection.invoke("GetLegend", legendId)
            .catch(function (err) {
                return console.error(err.toString());
            });
    }


    function GetAllowedHours() {

        connection.invoke("GetAllowedHours")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }



    function GetLegends() {

        connection.invoke("GetLegends")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }





    const holidays = [];

    connection.on("Holidays", function (holi) {
        var container = $(".tbodyHoliday").empty();

        holi.forEach(function (data) {
            holidays.push(data);
        });
    });

/*    console.log(holidays);*/

    connection.on("HolidaysByYear", function (holi) {

        var container = $(".tbodyHoliday").empty();

        if (holi.holiday.length > 0) {

            var sortedHolidays = holi.holiday.sort(function (a, b) {

                var aMonth = new Date(a.fixedDate).getMonth();
                var bMonth = new Date(b.fixedDate).getMonth();

                if (aMonth !== bMonth) {
                    return aMonth - bMonth;
                } else {
                    var aDay = new Date(a.fixedDate).getDate();
                    var bDay = new Date(b.fixedDate).getDate();
                    return aDay - bDay;
                }
            });

            if (holi.role == "Admin") {
                sortedHolidays.forEach(function (data) {

                    var date = new Date(data.fixedDate);
                    var fixedDate = date.toLocaleDateString("en-US", {
                        month: "long",
                        day: "numeric"
                    });

                    var html = `
                    <tr>
                        <td style="font-weight:bold;">${data.fixedName}</td>
                        <td>${fixedDate}</td>
                        <td>
                          ${data.holidayStatus.statusName === 'movable' ? '<i class="fa fa-pencil edit-holiday" data-id="' + data.fixedId + '" data-name="' + data.fixedName + '" data-date="' + data.fixedDate + '" aria-hidden="true" data-bs-toggle="modal" data-bs-target="#updateHolidayModal"></i> <i class="fa fa-trash delete-holiday" data-name="' + data.fixedName + '" data-id="' + data.fixedId + '" aria-hidden="true" data-bs-toggle="modal" data-bs-target="#deleteHolidayModal"></i>' : ''}
                         </td>
                    </tr>
                `;

                    container.append(html);

                    $(".edit-holiday").on("click", function () {

                        var id = $(this).data("id");
                        var date = $(this).data("date");
                        var movableName = $(this).data("name");



                        if (movableNames.includes(movableName)) {
                            $("#holidayId").val(id);
                            $("#updateHolidayDate").val(date);
                            $("#updateHolidaySelect").val(movableName);
                        }
                        else {
                            $("#holidayId").val(id);
                            $("#updateHolidayDate").val(date);
                            $("#updateHolidaySelect").val("Additional Special Non-Working");
                            $("#updateHolidayName").val(movableName);

                            $("#updateHolidayName").css("display", "block");
                            $("#holidayNameRequired").css("display", "block");
                            $(".holidayUpdateBtn").prop("disabled", false);
                            $(".holidayUpdateBtn").css("background-color", "#05513b");
                            $(".holidayUpdateBtn").css("cursor", "pointer");
                        }
                
                    


                    });
                    $(".delete-holiday").on("click", function () {

                        var holidayId = $(this).data("id");
                        var movableName = $(this).data("name");

                        $(".holidayIdToDelete").val(holidayId);
                        $("#holidayToDelete").text(movableName);

                    });


                });
            }
            else {

                sortedHolidays.forEach(function (data) {

                    var date = new Date(data.fixedDate);
                    var fixedDate = date.toLocaleDateString("en-US", {
                        month: "long",
                        day: "numeric"
                    });

                    var html = `
                    <tr>
                        <td style="font-weight:bold;">${data.fixedName}</td>
                        <td>${fixedDate}</td>
                        <td>
                          ${data.holidayStatus.statusName === 'movable' ? '<i class="fa fa-times edit-holiday"  aria-hidden="true"  style="pointer-events: none;"></i> <i class="fa fa-times delete-holiday"  aria-hidden="true" data-bs-toggle="modal" data-bs-target="#deleteHolidayModal" style="pointer-events: none;"></i>' : ''}
                         </td>
                    </tr>
                `;

                    container.append(html);


                });

            }

        }
        else {
            var htmls = `
            <div style="width: 60%; height: 55%; position: absolute;display:flex; justify-content:center; align-items:center;">
                <h1 style="color:#425833;">No Data</h1>
            </div>
        `;

            container.append(htmls);
        }
    });







    function GetReadyHoliday(year) {

        connection.invoke("GetHolidaysToShow", currentYear)
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



    function GetAllDataSheetSort(sectionId, name, year) {

        connection.invoke("GetAllDataSheetSort", sectionId, name, year)
            .catch(function (err) {
                return console.error(err.toString());
            });
    }


    function GetUserMonthlyStatisticsSort(sectionId, name, year) {

        connection.invoke("GetUsersMonthlyStatisticsSort", sectionId, name, year)
            .catch(function (err) {
                return console.error(err.toString());
            });
    }


    $("#dashboardYear, #dashboardSearch, #dashboardSectionDropDown").on("input", function () {

        var name = $("#dashboardSearch").val();

        var years = $("#dashboardYear").val();
        var year = parseInt(years);

        var section = $("#dashboardSectionDropDown").val();
        var sectionId = parseInt(section);


        if (name.length == 0 && section == "SECTION") {

            //$("#legendLow").text("");
            //$("#legendMed").text("");
            //$("#legendMax").text("");
            GetLegend(sectionId);

            connection.invoke("GetUsersMonthlyStatisticsSort", null, null, year)
                .catch(function (err) {
                    return console.error(err.toString());
                });
        }
        else if (section != "SECTION" && name.length == 0) {

            GetLegend(sectionId);

            connection.invoke("GetUsersMonthlyStatisticsSort", sectionId, null, year)
                .catch(function (err) {
                    return console.error(err.toString());
                });
        }
        else if (section == "SECTION" && name.length > 0) {

            //$("#legendLow").text("");
            //$("#legendMed").text("");
            //$("#legendMax").text("");
            GetLegend(sectionId);

            connection.invoke("GetUsersMonthlyStatisticsSort", null, name, year)
                .catch(function (err) {
                    return console.error(err.toString());
                });

        }
        else if (section != "SECTION" && name.length > 0) {

            GetLegend(sectionId);

            connection.invoke("GetUsersMonthlyStatisticsSort", sectionId, name, year)
                .catch(function (err) {
                    return console.error(err.toString());
                });

        }

    });




    $("#dataSheetYear, #dataSheetSearch, #dataSheetSectionDropDown").on("input", function () {

        var name = $("#dataSheetSearch").val();

        var years = $("#dataSheetYear").val();
        var year = parseInt(years);

        var section = $("#dataSheetSectionDropDown").val();
        var sectionId = parseInt(section);


        if (name.length == 0 && section == "SECTION") {

            connection.invoke("GetAllDataSheetSort", null, null, year)
                .catch(function (err) {
                    return console.error(err.toString());
                });

        }
        else if (section != "SECTION" && name.length == 0) {

            connection.invoke("GetAllDataSheetSort", sectionId, null, year)
                .catch(function (err) {
                    return console.error(err.toString());
                });
        }
        else if (section == "SECTION" && name.length > 0) {

            connection.invoke("GetAllDataSheetSort", null, name, year)
                .catch(function (err) {
                    return console.error(err.toString());
                });

        }
        else if (section != "SECTION" && name.length > 0) {

            connection.invoke("GetAllDataSheetSort", sectionId, name, year)
                .catch(function (err) {
                    return console.error(err.toString());
                });
        }


    });





    $("#holidayYear").on("change", function () {

        var years = $("#holidayYear").val();

        var year = parseInt(years);

        connection.invoke("GetHolidaysToShow", year)
            .catch(function (err) {
                return console.error(err.toString());
            });


    });


    function GetProjects() {
        connection.invoke("GetProjects")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }

    function GetAllDataSheet() {
        connection.invoke("GetAllDataSheet")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }


    function GetMovableNames() {
        connection.invoke("GetMovableNames")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }


    function GetBusinessOrIt() {
        connection.invoke("GetBusinessOrIt")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }

    const movableNames = [];

    connection.on("MovableNames", function (names) {

        var container = $("#addHolidaySelect");

        var container1 = $("#updateHolidaySelect");

        let html1 = `<option disabled selected>SELECT</option>`;

        container.empty();
        container.append(html1);

  

        names.forEach(function (data) {

            movableNames.push(data.name);

            let html = `
                  <option value="${data.name}">${data.name}</option>
            `;

            container.append(html);
            container1.append(html);

        });


        container.on("change", function () {
            var value = $(this).val(); // Use $(this) to reference the current element

            if (value == "Additional Special Non-Working") {
                $("#addHolidayName").css("display", "block");
                $("#holidayNameRequired").css("display", "block");
                $(".holidayAddBtn").prop("disabled", true);
                $(".holidayAddBtn").css("background-color", "gray");
                $(".holidayAddBtn").css("cursor", "not-allowed");

            } else {
                $("#addHolidayName").css("display", "none");
                $("#holidayNameRequired").css("display", "none");
                $(".holidayAddBtn").prop("disabled", false);
                $(".holidayAddBtn").css("background-color", "#05513b");
                $(".holidayAddBtn").css("cursor", "pointer");
                $("#addHolidayName").val("");

            }
        });



        container1.on("change", function () {

            var value = $(this).val();

            if (value == "Additional Special Non-Working") {

                $("#updateHolidayName").css("display", "block");
                $("#holidayNameRequired").css("display", "block");
                $(".holidayUpdateBtn").prop("disabled", true);
                $(".holidayUpdateBtn").css("background-color", "gray");
                $(".holidayUpdateBtn").css("cursor", "not-allowed");

            }
            //else if (!validOptions.includes(value)) {

            //    $("#updateHolidayName").css("display", "none");
            //    $("#holidayNameRequired").css("display", "none");
            //    $(".holidayUpdateBtn").prop("disabled", false);
            //    $(".holidayUpdateBtn").css("background-color", "#05513b");
            //    $(".holidayUpdateBtn").css("cursor", "pointer");
            //    $("#updateHolidayName").val("");

            //}
            else {
                $("#updateHolidayName").css("display", "none");
                $("#holidayNameRequired").css("display", "none");
                $(".holidayUpdateBtn").prop("disabled", false);
                $(".holidayUpdateBtn").css("background-color", "#05513b");
                $(".holidayUpdateBtn").css("cursor", "pointer");
                $("#updateHolidayName").val("");

            }


        });


    });

    
    $(".closeUpdateModal").on("click", function () {

        $("#updateHolidayName").css("display", "none");
        $("#holidayNameRequired").css("display", "none");
        $(".holidayUpdateBtn").prop("disabled", false);
        $(".holidayUpdateBtn").css("background-color", "#05513b");
        $(".holidayUpdateBtn").css("cursor", "pointer");
        $("#updateHolidayName").val("");
    });



    $("#addHolidayName").on("input", function(){

        var name = $("#addHolidayName").val();

        if (name.length > 0) {
            $(".holidayAddBtn").prop("disabled", false);
            $(".holidayAddBtn").css("background-color", "#05513b");
            $(".holidayAddBtn").css("cursor", "pointer");
        }
        else {
            $(".holidayAddBtn").prop("disabled", true);
            $(".holidayAddBtn").css("background-color", "gray");
            $(".holidayAddBtn").css("cursor", "not-allowed");

        }
    });

    $("#updateHolidayName").on("input", function () {

        var name = $("#updateHolidayName").val();

        if (name.length > 0) {
            $(".holidayUpdateBtn").prop("disabled", false);
            $(".holidayUpdateBtn").css("background-color", "#05513b");
            $(".holidayUpdateBtn").css("cursor", "pointer");
        }
        else {
            $(".holidayUpdateBtn").prop("disabled", true);
            $(".holidayUpdateBtn").css("background-color", "gray");
            $(".holidayUpdateBtn").css("cursor", "not-allowed");

        }
    });


    connection.on("Business", function (names) {

        var container = $("#addDataSheetBusinessDropdown");

        var container1 = $("#updateDataSheetBusinessOrIT");

        names.forEach(function (data) {

            let html = `
                  <option value="${data.businessOrItName}">${data.businessOrItName}</option>
            `;

            container.append(html);
            container1.append(html);


        });


    });

    let userDetailsSection;

    connection.on("UserDetails", function (user) {

        userDetailsSection = user.section.sectionId;

        $("#dashboardSectionDropDown").val(userDetailsSection);

    });


    connection.on("ReceiveAllRoles", function (names) {

        $("#addNewUserRoles").empty();

        let htmluser11 = `<option disabled selected> Role </option>`;
        $("#addNewUserRoles").append(htmluser11);

        names.forEach(function (data) {

            if (data === "Manager" || data === "Project_Manager") {
                let html = `<option value="${data}">${data}</option>`;
                $("#addNewUserRoles").append(html);
            }
        });


    });

    $("#addNewUserRoles").on("change", function () {

        var value = $("#addNewUserRoles").val();

        if (value == "Manager") {

            $("#addNewUserSectionDrop").css("display", "block");
            $("#addNewUserSectionDropValidation").css("display", "block");
            $("#addNewUserRoles").css("width", "103px");

        }
        else {
            
            $("#addNewUserSectionDrop").css("display", "none");
            $("#addNewUserSectionDropValidation").css("display", "none");
            $("#addNewUserRoles").css("width", "173px");
        }

    });

    $("#addNewUserSectionDrop").on("change", function () {

        var text = $("#addNewUserSectionDrop option:selected").text();

        if (text == "BEST") {
            $("#addNewPositionUserDisabled").val("Project Manager");
            $("#addNewPositionUser").val("Project Manager");
            $("#addNewUserRole").val("Project_Manager");
        }
        else {
            $("#addNewPositionUserDisabled").val("Manager");
            $("#addNewPositionUser").val("Manager");
            $("#addNewUserRole").val("Manager");
        }



        //if (value == "Manager") {

        //    $("#addNewUserSectionDrop").css("display", "block");
        //    $("#addNewUserSectionDropValidation").css("display", "block");
        //    $("#addNewUserRoles").css("width", "103px");

        //}
        //else {

        //    $("#addNewUserSectionDrop").css("display", "none");
        //    $("#addNewUserSectionDropValidation").css("display", "none");
        //    $("#addNewUserRoles").css("width", "173px");
        //}

    });


    connection.on("AllProjects", function (projects) {

        var containerForDisplayingProjects = $(".projectsModalDiv2TableBody");

        projects.forEach(function (data) {



            let htmlForDisplay = `
                <tr>
                    <td>${data.projectName}</td>
                    <td>${data.appUser.fullName}</td>
                    <td>${data.projects_ActivitiesStatus.statusName}</td>
                    <td>
                        <i class="fa fa-pencil editProjectsPencil" aria-hidden="true" data-bs-toggle="modal" data-bs-target="#updateProject1" data-pmname="${data.appUserId}" data-id="${data.projectId}" data-name="${data.projectName}" data-status="${data.projects_ActivitiesStatus.statusName}"></i>
                    </td>
                </tr>
            `;

            containerForDisplayingProjects.append(htmlForDisplay);


            $(".editProjectsPencil").on("click", function () {

                var id = $(this).data("id");
                var projectName= $(this).data("name");
                var status = $(this).data("status");
                var pmName = $(this).data("pmname");

                $("#projectId").val(id);
                $("#updateProjectName").val(projectName);
                $("#updateProjectStatus").val(status);
                $("#updateProjectManagerName ").val(pmName);
                
            });

        });
    });

    connection.on("AllActiveProjects", function (projects) {

        var container = $("#addDataSheetProjectsDropdown");

        var container1 = $("#updateDataSheetProjectName");

        projects.forEach(function (data) {

            let html = `
                  <option value="${data.projectName}">${data.projectName}</option>
            `;


            container.append(html);
            container1.append(html);


        });
    });




    connection.on("AllActivities", function (activities) {

        var container = $("#addDataSheetActivitiesDropdown");
        var container1 = $("#updateDataSheetActivityName");

        var containerForDisplayingActivties = $(".activitiesModalDiv2TableBody");


        activities.forEach(function (data) {



            if (data.projects_ActivitiesStatus.statusName == "ACTIVE") {
                let html = `
                  <option value="${data.activityName}">${data.activityName}</option>
                 `;

                container.append(html);
                container1.append(html);

            }


            let htmlForDisplay = `
                <tr>
                    <td>${data.activityName}</td>
                    <td>${data.projects_ActivitiesStatus.statusName}</td>
                    <td>
                        <i class="fa fa-pencil editActivitiesPencil" aria-hidden="true" data-bs-toggle="modal" data-bs-target="#updateProject" data-id="${data.activityId}" data-name="${data.activityName}" data-status="${data.projects_ActivitiesStatus.statusName}"></i>
                    </td>
                </tr>
            `;



            containerForDisplayingActivties.append(htmlForDisplay);

            $(".editActivitiesPencil").on("click", function () {

                var id = $(this).data("id");
                var projectName = $(this).data("name");
                var status = $(this).data("status");


                $("#activityId").val(id);
                $("#updateActivityName").val(projectName);
                $("#updateActivityStatus").val(status);


            });


        });
    });


    connection.on("AllProjectManager", function (pmanagers) {

        var container = $("#addProjectPmanager");
        var container1 = $("#updateProjectManagerName");
        container.empty();
        container1.empty();

        let top = `
               <option selected disabled>Select Project Manager</option>
        `;

        container.append(top);
        container1.append(top);

        pmanagers.forEach(function (data) {

            let html = `
                  <option value="${data.id}">${data.fullName}</option>
            `;

            container.append(html);
            container1.append(html);

        });

    });

    connection.on("AllProjectManagersProject", function (projects) {

        var container = $(".projectManagerProjectsTableBody");

        container.empty();

        projects.forEach(function (data) {

            let htmlForDisplay = `
                <tr class="projectsPage" data-id="${data.projectId}" data-name="${data.projectName}" data-bs-dismiss="modal">
                    <td>
                            ${data.projectName}
                 
                    </td>
                    <td>${data.projects_ActivitiesStatus.statusName}</td>               
                </tr>
            `;

            container.append(htmlForDisplay);



        });

        $(".projectsPage").on("click", function () {

            var id = $(this).data("id");

            var name = $(this).data("name");

            projectManagerProjectId = id;

            let years = $("#projectsYear").val();

            let year = parseInt(years);

            connection.invoke("GetUserMonthlyStatisticsSortProjects", id, year, null, null)
                .catch(function (err) {
                    return console.error(err.toString());
                });

            $("#ProjectTitle").empty();

            $("#ProjectTitle").html(name);

        });

    });


    connection.on("UserMonthlyStatisticsSortProjects", function (users) {

        var container = $(".tbodyProjects").empty();


        if (users.length > 0) {

            container.empty();

            users.forEach(function (user) {

                let html = `
                     <tr>
                        <td>${user.sectionName}</td>
                        <td>${user.appUserName}</td>
                        <td  style="${getBackgroundColor(user.january, user.low, user.med, user.max)};">${user.january}</td>
                        <td  style="${getBackgroundColor(user.february, user.low, user.med, user.max)};">${user.february} </td>
                        <td  style="${getBackgroundColor(user.march, user.low, user.med, user.max)};">${user.march} </td>
                        <td  style="${getBackgroundColor(user.april, user.low, user.med, user.max)};">${user.april} </td>
                        <td  style="${getBackgroundColor(user.may, user.low, user.med, user.max)};">${user.may} </td>
                        <td  style="${getBackgroundColor(user.june, user.low, user.med, user.max)};">${user.june} </td>
                        <td  style="${getBackgroundColor(user.july, user.low, user.med, user.max)};">${user.july} </td>
                        <td  style="${getBackgroundColor(user.august, user.low, user.med, user.max)};">${user.august} </td>
                        <td  style="${getBackgroundColor(user.september, user.low, user.med, user.max)};">${user.september} </td>
                        <td  style="${getBackgroundColor(user.october, user.low, user.med, user.max)};">${user.october} </td>
                        <td style="${getBackgroundColor(user.november, user.low, user.med, user.max)};">${user.november} </td>
                        <td  style="${getBackgroundColor(user.december, user.low, user.med, user.max)};">${user.december} </td>
                        <td >${user.totalHours} </td>
                    </tr>
                `;

                container.append(html);

            });
        }
        else {

            container.empty();

            let htmls = `<div style="width: 80%; height: 60%; position: absolute;display:flex; justify-content:center; align-items:center;">
                <h1 style="color:#425833;"><span style="font-weight:bold;">No Data Found!</h1>
            </div>`;

            container.append(htmls);
        }

    });


    $("#projectsYear").on("change", function () {

        if (projectManagerProjectId != 0) {

            let years = $("#projectsYear").val();

            let year = parseInt(years);

            connection.invoke("GetUserMonthlyStatisticsSortProjects", projectManagerProjectId, year, null, null)
                .catch(function (err) {
                    return console.error(err.toString());
                });

        }
        else {
            $("#resultProject").css("left", "35%");
            $("#resultProject").html("S E L E C T &nbsp; P R O J E C T &nbsp; F I R S T!");
        }

    });

    function GetActivities() {
        connection.invoke("GetActivities")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }



    function GetUserDetails() {
        connection.invoke("GetUserDetails")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }

    function GetAllRoles() {
        connection.invoke("GetAllRoles")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }

    function GetActiveActivities() {
        connection.invoke("GetActiveActivities")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }

    function GetActiveProjects() {
        connection.invoke("GetActiveProjects")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }

    function GetAllProjectManager() {
        connection.invoke("GetAllProjectManager")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }

    function GetProjectManagersProject() {
        connection.invoke("GetProjectManagerProject")
            .catch(function (err) {
                return console.error(err.toString());
            });
    }

    connection.start().then(function () {
        GetAllProjectManager();
        GetProjectManagersProject();

        GetActiveActivities();
        GetActiveProjects();

        GetActivities();
        GetProjects();
        GetUserDetails();
        GetAllRoles();

        GetAllUsers();
        GetSectionsDropdown();
        GetSectionsLegends();
        GetAllUsersCount();
        GetAllDataSheet();
        GetLegends();

        GetBusinessOrIt();

        GetMovableNames();

        GetHoliday();
        GetReadyHoliday(currentYear);

        GetUsersMonthlyStatistics();
        GetAllowedHours();

        GetAllDataSheetSort(null, null, currentYear);

        GetUserMonthlyStatisticsSort(null, null, currentYear);

        GetLegend(null);

    }).catch(function (err) {
            console.error(err.toString());
        });

});



//$.ajax({
//    type: 'GET',
//    url: "/Admin/AdminDashboardSearchDataList",
//    data: { input: userInput },
//    dataType: "json",
//    success: function (result) {
//        /*      alert(result);*/

//    },
//    error: function (req, status, error) {
//        console.log(status);
//    }
//});
