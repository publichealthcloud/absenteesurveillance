// QUARTZ HELPER FUNCTIONS

$(function () {
    $('#elementModal').on('show', function () {
        $('.modal-body', this).css({ width: 'auto', height: $(window).height()-300, 'max-height': '90%' });
        $('body').css('overflow', 'hidden');    // prevents scrolling of body
    }).on('hidden', function () {
        $('body').css('overflow', 'auto');
    });

    $('#manageElementModal').on('show', function () {
        $('.modal-body', this).css({ width: 'auto', height: $(window).height() - 300, 'max-height': '90%' });
        $('body').css('overflow', 'hidden');    // prevents scrolling of body
    }).on('hidden', function () {
        $('body').css('overflow', 'auto');
    });

    //$('FormArticleViewer').submit(function (e) { e.preventDefault(); });

    // remove li elements to the explore custom filter tools
    $(document).on('click', 'a.existing-search-filter', function () {
        var type = $(this).attr('data-value');
        $(this).parents('.filter-value').remove();

        // if no more items exist then display all and return show all button
        filter_items = $("#curr-explore-filters li");

        var num_existing_filters = 0;
        filter_items.each(function () {
            num_existing_filters++;
        });

        if (num_existing_filters >= 1) {
            FilterExplore();
            var header = document.getElementById('curr_selection');
            header.style.visibility = 'visible';
        } else {
            GetAvailableExploreFeed();
            var header = document.getElementById('curr_selection');
            header.style.visibility = 'hidden';
        }
    });

    // add li elements to the explore custom filter tools
    $("a.add-search-filter").on("click", function(event) {
        //event.preventDefault();
        var type = $(this).attr('data-value');
        var new_filter_html = '';
        new_filter_html = '<li data-value="' + type + '" class="filter-value"><a class="existing-search-filter" id="filter-'+type+'" href="#" data-value="' + type + '">' + type + ' <i class="icon-trash"></i></a></li>';

        // make sure the item hasn't already been added
        var filter_check = document.getElementById('filter-' + type);
        var ok_to_add = false;

        if (filter_check == null) {
            ok_to_add = true;
        }

        if (ok_to_add == true) {
            var $element = $(new_filter_html);
            $("#curr-explore-filters").append($element);

            // after adding item rerun filter code
            FilterExplore();

            filter_items = $("#curr-explore-filters li");
            var num_existing_filters = 0;
            filter_items.each(function () {
                num_existing_filters++;
            })

            if (num_existing_filters >= 1) {
                var header = document.getElementById('curr_selection');
                header.style.visibility = 'visible';
            }
        }
    });

    $('#userPopover').popover();
});

function closeManageElementModal() {
    $('#manageElementModal').modal('hide');
}

function openSchoolWindow(url)
{
    $("#s2")[0].selectedIndex = 0
    var win = window.open(url, '_blank');
    win.focus();
}

function openSchoolWindowGeneric(url) {
    var win = window.open(url, '_blank');
    win.focus();
}

function showManageElementModal(feed_id, reference_id, content_type_id, item_type, item_title) {
    item_title = decodeURI(item_title);

    // reset close code to insure that the correct settings are in place
    document.getElementById('#manage_element_close_top').setAttribute("onclick", "closeManageElementModal(); return false;");
    document.getElementById('#manage_element_close_bottom').setAttribute("onclick", "closeManageElementModal(); return false;");

    // decode special characters if present
    var temp_title = item_title.split("%3A").join(':');
    temp_title = temp_title.split("%3F").join('?');
    temp_title = temp_title.split("%26").join('&');
    temp_title = temp_title.split("%2C").join(',');
    item_title = temp_title;

    var alt_modal = false;

    // remove prior data
    $('#manageElementModal').removeData('modal');
    $(this).empty()
    // get new data
    if (item_type == 'school') {
        //document.getElementById('loading').style.visibility = 'visible';
        document.getElementById('#mTitle').textContent = item_title;
        $('#manageElementModal').modal({ remote: '/manage/viewers/viewer-school.aspx?schoolID=' + reference_id })
    }
    else if (item_type == 'training') {
        document.getElementById('#mTitle').textContent = item_title;
        $('#manageElementModal').modal({ remote: '/social/viewers/modal-viewer-training.aspx?feedID=' + feed_id + '&referenceID=' + reference_id + '&contentTypeID=' + content_type_id })
    }
    else if (item_type == 'poll-campaign-results') {
        document.getElementById('#mTitle').textContent = "Poll Results";
        $('#manageElementModal').modal({ remote: '/social/viewers/modal-viewer-poll.aspx?feedID=' + feed_id + '&referenceID=' + reference_id + '&contentTypeID=' + content_type_id })
        // reset close button to force reset of entire parent window to insure that step display is correct
        document.getElementById('#element_close_top').setAttribute("onclick", "location.reload();");
        document.getElementById('#element_close_bottom').setAttribute("onclick", "location.reload();");
    }

    // display modal
    $("#manageElementModal").modal("show");
}

function viewMemberCampaignActivity(user_id, campaign_id) {
    var alt_modal = false;

    // remove prior data
    $('#activityModal').removeData('modal');
    $(this).empty()

    document.getElementById('#aTitle').textContent = 'Member Campaign Activity';
    $('#activityModal').modal({ remote: '/manage/viewers/viewer-campaign-activity.aspx?userID=' + user_id + '&campaignID=' + campaign_id })

    // display modal
    $("#activityModal").modal("show");
}

function RemoveSpaceUser(user_id, space_id, mode) {
     bootbox.confirm("Are you sure you remove this member?", function (result) {
         if (result == true) {
             Quartz.Services.Spaces.RemoveSpaceUser(user_id, space_id);

             if (mode == "member") {
                 window.location.href = "/manage/members/member-profile.aspx?userID=" + user_id + "&currTab=group&message=successfully%20updated%20user%20group";
            } else {
                 var row = document.getElementById('tr-' + user_id);
                 if (row != null) {
                     row.style.visibility = 'hidden';
                 }

                 var num_members = document.getElementById('member-count').textContent;
                 if (num_members != null) {
                     var count_members = parseInt(num_members);
                     count_members = count_members - 1;
                     javascript: document.getElementById('member-count').textContent = count_members;
                 }
            }
        }
    });

    return false;
}

function RemoveCampaignUser(user_id, campaign_id) {
    bootbox.confirm("Are you sure you remove this member? All user campaign activities will be lost. This action cannot be undone.", function (result) {
        if (result == true) {
            Quartz.Services.Campaign.RemoveCampaignUser(user_id, campaign_id);

            /*
            var row = document.getElementById('tr-' + user_id);
            if (row != null) {
                row.style.visibility = 'hidden';
            }

            var num_members = document.getElementById('member-count').textContent;
            if (num_members != null) {
                var count_members = parseInt(num_members);
                count_members = count_members - 1;
                javascript: document.getElementById('member-count').textContent = count_members;
            }
            */

            // refresh window
            window.open(window.location.href);
        }
    });

    return false;
}

function RemoveCampaignActivity(campaign_action_id, campaign_id) {
    bootbox.confirm("Are you sure you remove this campaign activity? This action cannot be undone.", function (result) {
        if (result == true) {
            Quartz.Services.Campaign.RemoveCampaignActivity(campaign_action_id, campaign_id);

            var row = document.getElementById('tr-' + campaign_action_id);
            if (row != null) {
                row.style.visibility = 'hidden';
            }
        }
    });

    return false;
}

function SetPrimaryGroup(user_id, space_id) {
    bootbox.confirm("Are you sure you remove this member?", function (result) {
        if (result == true) {
            Quartz.Services.Spaces.SetPrimaryGroup(user_id, space_id);

            window.location.href = "/manage/members/member-profile.aspx?userID=" + user_id + "&currTab=group&message=successfully%20updated%20user%20group";
        }
    });

    return false;
}

function MakePrimarySpace(user_id, space_id, user_space_id) {
    bootbox.confirm("Are you sure you make this the primary group?", function (result) {
        if (result == true) {
            Quartz.Services.Spaces.SetPrimarySpace(user_id, space_id, user_space_id, onMarkPrimarySpaceSuccess);
        }
    });

    return false;
}
function onMarkPrimarySpaceSuccess(result) {
    var user_id = result;
    window.location.href = "/manage/members/member-profile.aspx?userID=" + user_id + "&currTab=group&message=successfully%20updated%20user%20group";
}

function AddThought() {
    bootbox.confirm("<strong>NOTICE</strong><br><br>To add a thought, just share a thought from the main site using any Admin or Host account.<br><br>After sharing a thought, return to this list and use the manage tools if you need to make that thought available for use in a campaign.", function (result) {
    });

    return false;
}


/* SCHOOL DISTRICTS */
function saveReportSearch() {

}
function onSaveReportSearchSuccess(result) {
    var absentee_search_id = result;
    //redirect to the search page and include the querystring id
}


/* Medications */
function RemoveMedicationGroupLink(link_id, medication_group_id) {
    bootbox.confirm("Are you sure you remove this link? This action cannot be undone.", function (result) {
        if (result == true) {
            Quartz.Services.Medication.RemoveMedicationGroupLink(link_id, medication_group_id);

            // refresh window
            window.open(window.location.href);
        }
    });

    return false;
}

function RemoveMedicationGroupSideEffect(link_id, medication_group_id) {
    bootbox.confirm("Are you sure you remove this side effect? This action cannot be undone.", function (result) {
        if (result == true) {
            Quartz.Services.Medication.RemoveMedicationSideEffect(link_id, medication_group_id);

            // refresh window
            window.open(window.location.href);
        }
    });

    return false;
}

function RemoveMedication(medication_id) {
    bootbox.confirm("Are you sure you remove this medication? This action cannot be undone.", function (result) {
        if (result == true) {
            Quartz.Services.Medication.RemoveMedication(medication_id);

            // refresh window
            window.open(window.location.href);
        }
    });

    return false;
}



/* HELP */
function openHelp(help_topic) {
    //window.open('/manage/help/default.aspx?topic=' + help_topic, 'Help', 'width=960,height=680')
    //hide_loading_panel()
    window.open('/manage/help/faq.aspx?topic=' + help_topic, 'Help', 'width=960,height=680')
}

