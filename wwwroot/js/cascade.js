// Cascade.js - Maneja los filtros en cascada automáticamente
document.addEventListener('DOMContentLoaded', function () {
	// Buscar todos los selects con atributo data-cascade-url
	const cascadeSelects = document.querySelectorAll('[data-cascade-url]');

	cascadeSelects.forEach(select => {
		select.addEventListener('change', function () {
			const url = this.getAttribute('data-cascade-url');
			const childSelector = this.getAttribute('data-cascade-child');
			const selectedValue = this.value;

			if (!url || !childSelector || !selectedValue) return;

			const childSelect = document.querySelector(childSelector);
			if (!childSelect) return;

			// Limpiar opciones previas del select hijo
			childSelect.innerHTML = '<option value="">-- Seleccionar --</option>';

			// Si no hay valor seleccionado, no hacer request
			if (!selectedValue) return;

			// Hacer fetch para obtener las opciones
			fetch(`${url}?id=${selectedValue}`)
				.then(response => response.json())
				.then(data => {
					if (Array.isArray(data)) {
						data.forEach(item => {
							const option = document.createElement('option');
							option.value = item.id;
							option.textContent = item.nombre;
							childSelect.appendChild(option);
						});
					}
				})
				.catch(error => console.error('Error en cascada:', error));
		});
	});
});
