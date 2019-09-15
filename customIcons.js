function addText(element) {
	var svgElement = ((((element.parentElement).parentElement).parentElement).parentElement);
	svgElement.classList.add(document.getElementById('cartCount').value);
	svgElement.classList.forEach(function(item, index) {
		item = item.toLowerCase();
		element.textContent = item.toUpperCase();
	});
}

function setFileType(element) {
	var svgElement = ((element.parentElement).parentElement).parentElement;
	svgElement.classList.add(document.getElementById('fileType').value);
	svgElement.classList.forEach(function(item, index) {
		item = item.toLowerCase();
		element.textContent = item.toUpperCase();
	});
}