function massDeletePicklistValues() {
    var names =
        ``;

    var namesArray = names.split('\n');
    var links = document.getElementsByTagName('a');

    var delLinks = new Array();
    for (var i = 0; i < links.length; i++) {
        var link = links[i];

        if (link.innerHTML == "Del") {
            var title = link.title.split('-').slice(2).join('-');
            var linkName = title.substring(1);
            if (namesArray.indexOf(linkName) >= 0) {
                delLinks[delLinks.length] = link;
            }
        }
    }

    var exec = new deletionExecutor(delLinks);
    exec.performCall();
}

function deletionExecutor(linksToDelete) {
    this.i = 0;
    this.delLinks = linksToDelete;
}

deletionExecutor.prototype.performCall = function () {
    setTimeout(() => {
        if (this.i < this.delLinks.length) {
            var that = this;
            var delLink = this.delLinks[this.i];

            var getParams = delLink.attributes['href'].value.split('?')[1].split('&');
            var paramsMap = new Map();
            getParams.forEach(function (element) {
                var elementParams = element.split('=');
                paramsMap.put(elementParams[0], elementParams[1]);
            }, this);

            var title2 = delLink.title.split('-').slice(2).join('-');
            var linkName2 = title2.substring(1);

            var params = 'ReplaceValueWith=NullValue&setupid=OpportunityFields';
            params += '&id=' + paramsMap.get('id');
            params += '&delID=' + paramsMap.get('id');
            params += '&delID=+Save+';
            params += '&_CONFIRMATIONTOKEN=' + paramsMap.get('_CONFIRMATIONTOKEN');
            params += '&tid=' + paramsMap.get('tid');
            params += '&pt=' + paramsMap.get('pt');
            params += '&p1=' + linkName2;
            params += '&retURL=' + paramsMap.get('retURL');

            var xmlhttp = new XMLHttpRequest();
            xmlhttp.open("POST", '/setup/ui/picklist_masterdelete.jsp?setupid=OpportunityFields', true);
            xmlhttp.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded')
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4) {
                    if (xmlhttp.status == 200) {
                        console.log('Picklist value: ' + linkName2 + ' has been deleted!');
                        that.i += 1;
                        that.performCall();
                    } else {
                        console.log('Failed to delete picklist value: ' + linkName2);
                    }
                }
            }.bind(that);
            xmlhttp.send(params);
        }
    }, 3500);
}