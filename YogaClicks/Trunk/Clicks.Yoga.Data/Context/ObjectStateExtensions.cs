using System;
using System.Data;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Linq;

namespace Clicks.Yoga.Data
{
    public static class ObjectStateExtensions
    {
        public static void ValidateBelongsTo(this AssociationEndMember end, ObjectStateEntry entry)
        {
            if (!entry.IsRelationship) throw new ArgumentException("entry");

            var fieldMetadata = entry.UsableValues().DataRecordInfo.FieldMetadata;

            if (
                fieldMetadata[0].FieldType as AssociationEndMember != end &&
                fieldMetadata[1].FieldType as AssociationEndMember != end)
            {
                throw new InvalidOperationException(string.Format("Association end {0} does not participate in the relationship {1}.", end, entry));
            }
        }

        public static IExtendedDataRecord UsableValues(this ObjectStateEntry entry)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                case EntityState.Detached:
                case EntityState.Unchanged:
                case EntityState.Modified:
                    return (IExtendedDataRecord)entry.CurrentValues;
                case EntityState.Deleted:
                    return (IExtendedDataRecord)entry.OriginalValues;
                default:
                    throw new InvalidOperationException("This entity state should not exist.");
            }
        }

        public static AssociationEndMember[] GetAssociationEnds(this ObjectStateEntry entry)
        {
            var fieldMetadata = entry.UsableValues().DataRecordInfo.FieldMetadata;
            return fieldMetadata.Select(m => m.FieldType as AssociationEndMember).ToArray();
        }

        public static AssociationEndMember GetOtherAssociationEnd(this ObjectStateEntry entry, AssociationEndMember end)
        {
            end.ValidateBelongsTo(entry);
            AssociationEndMember[] ends = entry.GetAssociationEnds();
            if (ends[0] == end) return ends[1];
            return ends[0];
        }

        public static EntityKey GetEndEntityKey(this ObjectStateEntry entry, AssociationEndMember end)
        {
            end.ValidateBelongsTo(entry);
            AssociationEndMember[] ends = entry.GetAssociationEnds();
            if (ends[0] == end) return entry.UsableValues()[0] as EntityKey;
            return entry.UsableValues()[1] as EntityKey;
        }

        public static NavigationProperty GetNavigationProperty(this ObjectStateEntry entry, AssociationEndMember end)
        {
            end.ValidateBelongsTo(entry);

            var otherEnd = entry.GetOtherAssociationEnd(end);
            var relationshipType = entry.EntitySet.ElementType;
            var key = entry.GetEndEntityKey(end);
            var entitySet = key.GetEntitySet(entry.ObjectStateManager.MetadataWorkspace);
            var property = entitySet.ElementType.NavigationProperties.SingleOrDefault(
                p => p.RelationshipType == relationshipType &&
                p.FromEndMember == end && p.ToEndMember == otherEnd);

            return property;
        }
    }
}
