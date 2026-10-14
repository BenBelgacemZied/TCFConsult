# TCFConsult

Application MAUI de collecte de documents comptables, conçue pour évoluer vers une plateforme SaaS multi-cabinets.

## Premier incrément

- Scanner un document depuis Android.
- Générer un PDF localement.
- Préparer le dépôt du PDF vers Google Drive via une API sécurisée.
- Garder les règles métier indépendantes de MAUI et du fournisseur de stockage.

## Architecture cible

- `src/TCFConsult.Domain` : entités et règles métier.
- `src/TCFConsult.Application` : cas d’usage et ports.
- `src/TCFConsult.Infrastructure` : adaptateurs de stockage, API et persistance.
- `src/TCFConsult.Mobile` : interface .NET MAUI, scanner et navigation.
- `tests/` : tests unitaires et d’intégration.

Le premier stockage pourra utiliser le Google Drive personnel du propriétaire. Pour le SaaS, chaque cabinet devra connecter son propre Drive, avec OAuth côté serveur, stockage isolé par tenant et contrôle d’accès côté API. Les jetons OAuth ne doivent pas être intégrés dans l’application mobile.

## Statut

Le dépôt vient d’être initialisé. Le prochain incrément est le squelette compilable de la solution MAUI et le flux de scan vers PDF local.